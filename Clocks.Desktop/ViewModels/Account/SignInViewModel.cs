﻿using System.Windows.Input;
using Clocks.Desktop.Tools;
using Clocks.Desktop.Tools.Managers;
using Clocks.Desktop.Tools.Navigation;
using Clocks.Shared.DtoModels;

namespace Clocks.Desktop.ViewModels.Account
{
    internal class SignInViewModel: BaseViewModel
    {
        private string _login;
        private string _password;

        private ICommand _signInCommand;
        private ICommand _toSignUpCommand;
        private ICommand _closeCommand;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignInCommand =>
            _signInCommand ??= new RelayCommand<object>(SignInImplementation, CanSignInExecute);

        private async void SignInImplementation(object obj)
        {

        }

        private bool CanSignInExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(_login) &&
                 !string.IsNullOrWhiteSpace(_password);
        }

        public ICommand SignUpCommand =>
            _toSignUpCommand ??= new RelayCommand<object>(ToSignUpImplementation);
        private void ToSignUpImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.SignUp);
        }

        public ICommand CloseCommand =>
            _closeCommand ??= new RelayCommand<object>(CloseExecute);
        private void CloseExecute(object obj)
        {
            //StationManager.CloseApp();
        }
    }
}
