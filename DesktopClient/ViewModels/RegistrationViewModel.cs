using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common;
using Common.Utils;
using DesktopClient.Helpers;
using DesktopClient.Views;

namespace DesktopClient.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        public RegistrationViewModel(BaseViewModel parent) : base(parent)
        {
            RegisterCommand = new CommandHandler(Register, CanExecuteRegisterCommand);
        }

        public ICommand RegisterCommand { get; private set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value == _username)
                    return;
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        //private string _password;
        //public string Password
        //{
        //    get => _password;
        //    set
        //    {
        //        if (value == _password)
        //            return;
        //        _password = value;
        //        OnPropertyChanged(nameof(Password));
        //    }
        //}

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (value == _email)
                    return;
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private bool _hasError;
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

        private bool CanExecuteRegisterCommand(object sender)
        {
            return !(string.IsNullOrWhiteSpace(Username) && Validations.ValidateStringLength(Username, 8)
                     //|| string.IsNullOrWhiteSpace(StringUtility.SecureStringToStringConverter((sender as IHavePassword).Password))
                     || string.IsNullOrWhiteSpace(Email) && Validations.ValidateEmail(Email));
        }

        public void Register(object sender)
        {
            var passwordBox = sender as IHavePassword;
            if (passwordBox == null)
            {
                return;
            }

            var securePassword = passwordBox.Password;

            var salt = new byte[16];

            var user = new User
            {
                Username = Username,
                Password = StringUtility.HashString(StringUtility.SecureStringToStringConverter(securePassword), out salt),
                Email = Email,
                Salt = salt
            };

            Service.RegisterUser(user);

            if (sender is RegistrationWindow window)
                window.Close();
        }
    }
}
