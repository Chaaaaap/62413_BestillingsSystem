using System;
using System.Security;
using System.Windows.Input;
using DesktopClient.Helpers;
using DesktopClient.Views;

namespace DesktopClient.ViewModels
{
    class LoginViewModel : BaseViewModel
    {

        public LoginViewModel()
        {
            InitializeCommands();
        }
        RegistrationWindow _regWindow;

        public ICommand LoginCommand
        {
            get;
            private set;
        }

        public ICommand OpenRegisterCommand { get; private set; }

        private void InitializeCommands()
        {
            LoginCommand = new CommandHandler(Login);
            OpenRegisterCommand = new CommandHandler(OpenRegister);

        }
        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                if(_username == value)
                    return;
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private SecureString _password;

        public SecureString Password
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public void Login(object parameter)
        {
            var passwordBox = parameter as IHavePassword;
            if (passwordBox == null)
            {
                return;
            }

            var securePassword = passwordBox.Password;

            var currentUser = Service.Login(Username, securePassword).Result;

            if (currentUser != null)
            {
                ApplicationInfo.CurrentUser = currentUser;
            }
            else
                throw new ArgumentException();
        }

        private void OpenRegister(object parameter)
        {
            _regWindow = new RegistrationWindow();
            _regWindow.Show();
        }
    }
}
