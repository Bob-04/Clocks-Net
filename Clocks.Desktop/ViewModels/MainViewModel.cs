using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Clocks.Desktop.Tools;
using Clocks.Desktop.Tools.Managers;
using Clocks.Shared.DtoModels;

namespace Clocks.Desktop.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private string _userName;
        private ICommand _deleteCommand;
        private ICommand _addCommand;
        private ICommand _signOutCommand;
        private ICommand _closeCommand;

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

        public ObservableCollection<TimeZoneInfo> Timezones { get; set; }

        private Clock _selectedClock;

        public Clock SelectedClock
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
            _userName = StationManager.CurrentUser.Username;
            Timezones = new ObservableCollection<TimeZoneInfo>(TimeZoneInfo.GetSystemTimeZones());
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
            }
            catch (Exception e)
            {
                MessageBox.Show("Server unavailable");
            }

            LoaderManager.Instance.HideLoader();
        }

        async void Clock_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "Zone")
            //{
            //    LoaderManager.Instance.ShowLoader();
            //    await Task.Run(() =>
            //    {
            //        try
            //        {
            //            StationManager.WcfClient.UpdateClock(sender as Clock);
            //        }
            //        catch (System.ServiceModel.EndpointNotFoundException ex)
            //        {
            //            StationManager.LogError("Server unavailable");
            //            StationManager.LogError(ex.Message);
            //            MessageBox.Show("Server unavailable");
            //        }
            //    });
            //    LoaderManager.Instance.HideLoader();
            //}
        }

        #region Commands

        public ICommand AddClockCommand =>
            _addCommand ??= new RelayCommand<object>(AddExecute);

        private async void AddExecute(object obj)
        {
            //var newClock = new Clock(
            //    (Timezone)Enum.Parse(typeof(Timezone), Timezone.Utc0.ToString()),
            //    MyModel);
            //newClock.PropertyChanged += Clock_PropertyChanged;

            //LoaderManager.Instance.ShowLoader();
            //var result = await Task.Run(() =>
            //{
            //    try
            //    {
            //        StationManager.WcfClient.AddClock(newClock);
            //    }
            //    catch (System.ServiceModel.EndpointNotFoundException ex)
            //    {
            //        StationManager.LogError("Server unavailable");
            //        StationManager.LogError(ex.Message);
            //        MessageBox.Show("Server unavailable");
            //        return false;
            //    }
            //    return true;
            //});
            //LoaderManager.Instance.HideLoader();
            //if (result)
            //{
            //    Clocks.Add(newClock);
            //}
        }

        public ICommand DeleteClockCommand =>
            _deleteCommand ??= new RelayCommand<object>(DeleteExecute);

        private async void DeleteExecute(object obj)
        {
            //LoaderManager.Instance.ShowLoader();
            //var result = await Task.Run(() =>
            //{
            //    try
            //    {
            //        StationManager.WcfClient.DeleteClock(SelectedClock);
            //    }
            //    catch (System.ServiceModel.EndpointNotFoundException ex)
            //    {
            //        StationManager.LogError("Server unavailable");
            //        StationManager.LogError(ex.Message);
            //        MessageBox.Show("Server unavailable");
            //        return false;
            //    }
            //    return true;
            //});
            //LoaderManager.Instance.HideLoader();
            //if (result)
            //{
            //    Clocks.Remove(SelectedClock);
            //}
        }

        public ICommand SignOutCommand =>
            _signOutCommand ??= new RelayCommand<object>(SignOutExecute);

        private async void SignOutExecute(object obj)
        {
            //LoaderManager.Instance.ShowLoader();
            //await Task.Run(() =>
            //{
            //    try
            //    {
            //        StationManager.WcfClient.UpdateLastSeenDateByLogin(StationManager.CurrentUser.Login);
            //    }
            //    catch (System.ServiceModel.EndpointNotFoundException ex)
            //    {
            //        StationManager.LogError("Server unavailable");
            //        StationManager.LogError(ex.Message);
            //        MessageBox.Show("Server unavailable," +
            //                        " your last seen date will not be saved");
            //    }
            //});
            //LoaderManager.Instance.HideLoader();

            //StationManager.CurrentUser = null;
            //StopUpdatingTimes();
            //SerializationManager.DeleteSerialization();
            //NavigationManager.Instance.Navigate(ViewType.SignIn);
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
                foreach (var clock in Clocks)
                {
                    clock.CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(curUtcDateTime,
                        TimeZoneInfo.FindSystemTimeZoneById(clock.TimeZoneId));
                }

                Thread.Sleep(1000);
            }
        }
    }
}
