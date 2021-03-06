using System.Windows.Input;
using Clocks.Desktop.Tools;
using Clocks.Desktop.Tools.Managers;
using Clocks.Desktop.Tools.Navigation;
using Clocks.Shared.DtoModels.Account;

namespace Clocks.Desktop.ViewModels.Account
{
    internal class SignUpViewModel: BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _login;
        private string _password;
        private string _repeatedPassword;

        private ICommand _signUpCommand;
        private ICommand _backToSignInCommand;
        private ICommand _closeCommand;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
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
        public string RepeatedPassword
        {
            get => _repeatedPassword;
            set
            {
                _repeatedPassword = value;
                OnPropertyChanged();
            }
        }

        public ICommand CloseCommand => _closeCommand ??= new RelayCommand<object>(CloseExecute);

        public ICommand ToSignInCommand => _backToSignInCommand ??= new RelayCommand<object>(ToSignIn);

        public ICommand SignUpCommand => _signUpCommand ??= new RelayCommand<object>(SignUpExecute, CanSignUpExecute);

        private bool CanSignUpExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(_firstName)
                   && !string.IsNullOrWhiteSpace(_lastName)
                   && !string.IsNullOrWhiteSpace(_email)
                   && !string.IsNullOrWhiteSpace(_login)
                   && !string.IsNullOrWhiteSpace(_password)
                   && !string.IsNullOrWhiteSpace(_repeatedPassword);
        }

        private async void SignUpExecute(object obj)
        {
            LoaderManager.Instance.ShowLoader();

            var user = await StationManager.ServerClient.SignUp(new SignUpRequest
            {
                Username = _login,
                FirstName = _firstName,
                LastName = _lastName,
                Email = _email,
                Password = _password,
                RepeatPassword = _repeatedPassword
            });

            LoaderManager.Instance.HideLoader();

            if (user != null)
            {
                StationManager.CurrentUser = user;
                NavigationManager.Instance.Navigate(ViewType.Main);
            }
        }

        private void ToSignIn(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.SignIn);
        }

        private void CloseExecute(object obj)
        {
            StationManager.CloseApp();
        }
    }
}
