﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Clocks.Desktop.Tools;
using Clocks.Desktop.Tools.Managers;
using Clocks.Desktop.Tools.Navigation;
using Clocks.Shared.DtoModels;

namespace Clocks.Desktop.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private ICommand _deleteCommand;
        private ICommand _addCommand;
        private ICommand _signOutCommand;
        private ICommand _closeCommand;

        public string Username { get; }

        private ObservableCollection<ClockDto> _clocks;

        public ObservableCollection<ClockDto> Clocks
        {
            get => _clocks;
            set
            {
                _clocks = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> TimezoneIds { get; set; }

        private ClockDto _selectedClock;

        public ClockDto SelectedClock
        {
            get => _selectedClock;
            set
            {
                _selectedClock = value;
                OnPropertyChanged();
            }
        }

        private readonly Thread _updatingTimeThread;
        private bool _updatingTimeThreadAvailable;

        internal MainViewModel()
        {
            Username = StationManager.CurrentUser?.Username;
            TimezoneIds = new ObservableCollection<string>(TimeZoneInfo.GetSystemTimeZones().Select(t => t.Id));
            Clocks = new ObservableCollection<ClockDto>();
            InitClocks();
            _updatingTimeThreadAvailable = true;
            (_updatingTimeThread = new Thread(UpdateTimes)).Start();
        }

        private async void InitClocks()
        {
            LoaderManager.Instance.ShowLoader();

            try
            {
                var clocks = await StationManager.ServerClient.GetUserClocks();
                Clocks = new ObservableCollection<ClockDto>(clocks);
                foreach (var clock in Clocks)
                {
                    clock.PropertyChanged += Clock_PropertyChanged;
                }
            }
            catch (HttpRequestException)
            {
                Clocks = new ObservableCollection<ClockDto>();
                MessageBox.Show("Server unavailable");
            }

            LoaderManager.Instance.HideLoader();
        }

        async void Clock_PropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName == nameof(ClockDto.Name) ||
                eventArgs.PropertyName == nameof(ClockDto.TimeZoneId))
            {
                LoaderManager.Instance.ShowLoader();

                try
                {
                    await StationManager.ServerClient.EditClock(sender as ClockDto);
                }
                catch (HttpRequestException)
                {
                    MessageBox.Show("Server unavailable");
                }

                LoaderManager.Instance.HideLoader();
            }
        }

        #region Commands

        public ICommand AddClockCommand =>
            _addCommand ??= new RelayCommand<object>(AddExecute);

        private async void AddExecute(object obj)
        {
            var newClock = new ClockDto
            {
                Name = "new clock",
                TimeZoneId = TimeZoneInfo.Utc.Id
            };

            LoaderManager.Instance.ShowLoader();

            try
            {
                var clockId = await StationManager.ServerClient.AddClock(newClock);
                if (clockId != Guid.Empty)
                {
                    newClock.Id = clockId;
                    newClock.PropertyChanged += Clock_PropertyChanged;
                    Clocks.Add(newClock);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Server unavailable");
            }

            LoaderManager.Instance.HideLoader();
        }

        public ICommand DeleteClockCommand =>
            _deleteCommand ??= new RelayCommand<object>(DeleteExecute);

        private async void DeleteExecute(object obj)
        {
            LoaderManager.Instance.ShowLoader();

            try
            {
                var result = await StationManager.ServerClient.RemoveClock(SelectedClock);
                if (result)
                {
                    Clocks.Remove(SelectedClock);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Server unavailable");
            }

            LoaderManager.Instance.HideLoader();
        }

        public ICommand SignOutCommand =>
            _signOutCommand ??= new RelayCommand<object>(SignOutExecute);

        private async void SignOutExecute(object obj)
        {
            LoaderManager.Instance.ShowLoader();

            try
            {
                await StationManager.ServerClient.SignOut();
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Server unavailable");
            }

            LoaderManager.Instance.HideLoader();

            StationManager.CurrentUser = null;
            StopUpdatingTimes();

            NavigationManager.Instance.Navigate(ViewType.SignIn);
        }

        public ICommand CloseCommand =>
            _closeCommand ??= new RelayCommand<object>(CloseAppExecute);

        private void CloseAppExecute(object obj)
        {
            StopUpdatingTimes();
            StationManager.CloseApp();
        }

        #endregion

        private async void StopUpdatingTimes()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                try
                {
                    _updatingTimeThreadAvailable = false;
                    _updatingTimeThread.Join(1010);
                    _updatingTimeThread.Abort();
                }
                catch
                {
                    // ignored
                }
            });
            LoaderManager.Instance.HideLoader();
        }

        private void UpdateTimes()
        {
            while (_updatingTimeThreadAvailable && Thread.CurrentThread.IsAlive)
            {
                var curUtcDateTime = DateTime.UtcNow;
                foreach (var clock in Clocks.Where(c => c.TimeZoneId != null))
                {
                    clock.CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(curUtcDateTime,
                        TimeZoneInfo.FindSystemTimeZoneById(clock.TimeZoneId)).ToLongTimeString();
                }

                Thread.Sleep(1000);
            }
        }
    }
}
