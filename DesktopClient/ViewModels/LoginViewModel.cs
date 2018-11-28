using System;
using System.Security;
using System.Windows;
using System.Windows.Input;
using Common.Utils;
using DesktopClient.Helpers;
using DesktopClient.Views;

namespace DesktopClient.ViewModels
{
    class LoginViewModel : BaseViewModel
    {

        public LoginViewModel(BaseViewModel parent) : base(parent)
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
            LoginCommand = new CommandHandler(Login, CanExecuteLogin);
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

        private bool _hasError = false;

        public bool HasError
        {
            get => _hasError;
            set
            {
                if (value == _hasError)
                    return;
                _hasError = value;
                OnPropertyChanged(nameof(HasError));
            }
        }

        public bool CanExecuteLogin(object sender)
        {
            return !string.IsNullOrWhiteSpace(Username);
        }

        public async void Login(object sender)
        {
            var passwordBox = sender as IHavePassword;
            if (passwordBox == null)
            {
                return;
            }

            var securePassword = passwordBox.Password;

            var currentUser = await Service.Login(Username, securePassword);

            if (currentUser != null)
            {
                ApplicationInfo.CurrentUser = currentUser;
                if (sender is LoginWindow window)
                {
                    Window mainWindow = new MainWindow();
                    mainWindow.Show();
                    window.Close();
                }
            }
            else
                HasError = true;


        }

        private void OpenRegister(object parameter)
        {
            _regWindow = new RegistrationWindow();
            _regWindow.Show();
        }
    }
}
