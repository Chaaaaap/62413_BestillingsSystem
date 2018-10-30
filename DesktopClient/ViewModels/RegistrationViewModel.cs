using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common;
using DesktopClient.Helpers;

namespace DesktopClient.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        public RegistrationViewModel()
        {
            RegisterCommand = new CommandHandler(Register);
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

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (value == _password)
                    return;
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
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

        public void Register(object parameter)
        {
            var passwordBox = parameter as IHavePassword;
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
        }
    }
}
