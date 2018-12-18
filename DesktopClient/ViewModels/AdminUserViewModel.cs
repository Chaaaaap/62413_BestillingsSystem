using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Common.Models;

namespace DesktopClient.ViewModels
{
    class AdminUserViewModel : BaseViewModel
    {
        public AdminUserViewModel(BaseViewModel parent) : base(parent)
        {
            InitializeCommands();
            PopulateUsers();
        }

        private User _selectedUser;

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                EditUser = true;
                CreateUser = false;
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

        private string _tmpSearch;

        public string TmpSearch
        {
            get => _tmpSearch;
            set
            {
                if (_tmpSearch == value)
                    return;
                _tmpSearch = value;
                OnPropertyChanged(nameof(TmpSearch));
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

        private bool _createUser = false;

        public bool CreateUser
        {
            get => _createUser;
            set
            {
                if (_createUser == value)
                    return;
                _createUser = value;
                OnPropertyChanged(nameof(CreateUser));
            }
        }

        private bool _editUser = false;

        public bool EditUser
        {
            get => _editUser;
            set
            {
                if (_editUser == value)
                    return;
                _editUser = value;
                OnPropertyChanged(nameof(EditUser));
            }
        }

        public ICommand SaveUserCommand
        {
            get;
            private set;
        }

        public ICommand CreateUserCommand
        {
            get;
            private set;
        }

        public ICommand AdminCreateUserCommand
        {
            get;
            private set;
        }
        public ICommand DeleteUserCommand
        {
            get;
            private set;
        }

        public ICommand SearchUserCommand
        {
            get;
            private set;
        }

        public ICommand ClearCommand
        {
            get;
            private set;
        }

        private ObservableCollection<User> _users = new ObservableCollection<User>();

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

        public async void Clear(object sender)
        {
            TmpSearch = "";
            await PopulateUsers();
        }
        
        private void InitializeCommands()
        {
            SaveUserCommand = new CommandHandler(SaveUserChanges);
            CreateUserCommand = new CommandHandler(AdminCreateUser);
            AdminCreateUserCommand = new CommandHandler(CreateUserClicked);
            DeleteUserCommand = new CommandHandler(DeleteUser);
            SearchUserCommand = new CommandHandler(SearchUser);
            ClearCommand = new CommandHandler(Clear);
        }

        public async void SearchUser(object sender)
        {
            await PopulateUsers();
            var temp = Users.Select(x => x).Where(x => x.Username.ToLower().Contains(TmpSearch.ToLower()));

            List<User> userTemp = new List<User>();

            foreach (User user in temp)
            {
                userTemp.Add(user);
            }

            Users.Clear();

            foreach (User user in userTemp)
            {
                Users.Add(user);
            }
            OnPropertyChanged(nameof(Users));
        }

        public async void AdminCreateUser(object sender)
        {
            //Her skal laves tjek for validation: 
            User user = new User
            {
                Username = TmpUsername,
                Email = TmpEmail,
                IsAdmin = TmpIsAdmin ?? false,
                Password = TmpPassword
            };

            await Service.AdminCreateUser(user);
            TmpUsername = null;
            TmpEmail = null;
            TmpIsAdmin = null;
            TmpPassword = null;
            await PopulateUsers();
            CreateUser = false;
        }

        public async void DeleteUser(object sender)
        {
            await Service.DeleteUser(SelectedUser);
            await PopulateUsers();
        }
        public async void SaveUserChanges(object sender)
        {
            User user = new User
            {
                Username = TmpUsername ?? SelectedUser.Username,
                Email = TmpEmail ?? SelectedUser.Email,
                IsAdmin = TmpIsAdmin ?? SelectedUser.IsAdmin,
                Password = TmpPassword ?? SelectedUser.Password,
                LatestLogin = SelectedUser.LatestLogin,
                Id = SelectedUser.Id
            };

            var updatedUser = await Service.UpdateUser(user);
            await PopulateUsers();

            OnPropertyChanged(nameof(Users));
            OnPropertyChanged(nameof(SelectedUser));

            TmpUsername = null;
            TmpEmail = null;
            TmpIsAdmin = null;
            TmpPassword = null;
            EditUser = false;
        }

        private void CreateUserClicked(object sender)
        {
            TmpUsername = null;
            TmpEmail = null;
            TmpIsAdmin = null;
            TmpPassword = null;
            SelectedUser = null;
            CreateUser = true;
            EditUser = false;

        }

        private async Task<ObservableCollection<User>> PopulateUsers()
        {
            List<User> userList = await Service.GetAllUsers();
            Users.Clear();

            foreach (User user in userList)
            {
                Users.Add(user);
            }
            return Users;
        }
    }

}
