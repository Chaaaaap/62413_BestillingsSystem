using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    class AdminUserViewModel : BaseViewModel
    {
        public AdminUserViewModel(BaseViewModel parent) : base(parent)
        {
            InitializeCommands();
        }

        private User _selectedUser;

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser == value)
                    return;
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
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

        private bool? _tmpIsAdmin;

        public bool? TmpIsAdmin
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

        public ICommand SaveUserCommand
        {
            get;
            private set;
        }

        private ObservableCollection<User> _users = new ObservableCollection<User>
        {
            new User
            {
                Username = "Test"
            },
            new User
            {
                Username = "ny test"
            }
        };

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                if (_users == value)
                    return;
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        private void InitializeCommands()
        {
            SaveUserCommand = new CommandHandler(SaveUserChanges);
        }

        public void SaveUserChanges(object sender)
        {


            //if(sender is User user)
            //{
            //    //var updatedUser = Service.UpdateUser(user).Result;
            //    var tmpUser = Users.Where(u => u.Id == user.Id).FirstOrDefault();
            //    if(tmpUser != null)
            //    {
            //        Users.Remove(tmpUser);
            //    }
            //    Users.Add(user);
            //    SelectedUser = user;
            //    OnPropertyChanged(nameof(Users));
            //    OnPropertyChanged(nameof(SelectedUser));
            //}

            User user = new User
            {
                Username = TmpUsername ?? SelectedUser.Username,
                Email = TmpEmail ?? SelectedUser.Email,
                IsAdmin = TmpIsAdmin ?? SelectedUser.IsAdmin,
                Password = SelectedUser.Password,
                LatestLogin = SelectedUser.LatestLogin,
                Id = SelectedUser.Id
            };

            var updatedUser = Service.UpdateUser(user).Result;

            var tmpUser = Users.Where(u => u.Id == updatedUser.Id).FirstOrDefault();
            if (tmpUser != null)
            {
                Users.Remove(tmpUser);
            }

            Users.Add(updatedUser);
            OnPropertyChanged(nameof(Users));
            OnPropertyChanged(nameof(SelectedUser));

            TmpUsername = null;
            TmpEmail = null;
            TmpIsAdmin = null;
        }
        
    }

    //private async void PopulateUsers()
    //{
    //    List<User> userList = await Service.get 
    //} 
}
