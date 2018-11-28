using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    class AdminCreateUserViewModel : BaseViewModel
    {
        public AdminCreateUserViewModel(BaseViewModel parent) : base(parent)
        {
            InitializeCommands();
        }

        private string _tmpUsername;

        public string TmpUsername
        {
            get => _tmpUsername;
            set
            {
                if (_tmpUsername == value)
                    return;
                _tmpUsername = value;
                OnPropertyChanged(nameof(TmpUsername));
            }
        }

        private string _tmpEmail;

        public string TmpEmail
        {
            get => _tmpEmail;
            set
            {
                if (_tmpEmail == value)
                    return;
                _tmpEmail = value;
                OnPropertyChanged(nameof(TmpEmail));
            }
        }

        private string _tmpPassword;

        public string TmpPassword
        {
            get => _tmpPassword;
            set
            {
                if (_tmpPassword == value)
                    return;
                _tmpPassword = value;
                OnPropertyChanged(nameof(TmpPassword));
            }
        }

        private bool _tmpIsAdmin;

        public bool TmpIsAdmin
        {
            get => _tmpIsAdmin;
            set
            {
                if (_tmpIsAdmin == value)
                    return;
                _tmpIsAdmin = value;
                OnPropertyChanged(nameof(TmpIsAdmin));
            }
        }

        public ICommand CreateUserCommand
        {
            get;
            private set;
        }

        private void InitializeCommands()
        {
            CreateUserCommand = new CommandHandler(CreateUser);
        }

        public void CreateUser(object sender)
        {
            User user = new User
            {
                Username = TmpUsername,
                Email = TmpEmail,
                IsAdmin = TmpIsAdmin,
                Password = TmpPassword
            };

            Service.AdminCreateUser(user);
        }
    }
}
