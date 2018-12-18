using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common;
using Common.Models;
using DesktopClient.Views;

namespace DesktopClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ICommand _changePageCommand;

        private BaseViewModel _currentPageViewModel;
        private List<BaseViewModel> _pageViewModels;

        public ICommand LogOutCommand { get; private set; }

        public MainWindowViewModel(BaseViewModel parent) : base(parent)
        {
            LogOutCommand = new CommandHandler(Logout);
            PageViewModels.Add(new ItemOverviewViewModel(this));
            if(IsAdminVisible)
                PageViewModels.Add(new AdministratorViewModel(this));
            PageViewModels.Add(new OrderHistoryViewModel(this));

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        public bool IsAdminVisible
        {
            get => ApplicationInfo.CurrentUser.IsAdmin;
        }

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new CommandHandler(
                        p => ChangeViewModel((BaseViewModel)p),
                        p => p is BaseViewModel);
                }

                return _changePageCommand;
            }
        }

        public List<BaseViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<BaseViewModel>();

                return _pageViewModels;
            }
        }

        public BaseViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged(nameof(CurrentPageViewModel));
                }
            }
        }

        private void ChangeViewModel(BaseViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void Logout(object sender)
        {
            if (sender is MainWindow mainWindow)
            {
                ApplicationInfo.CurrentUser = null;

                var loginWindow = new LoginWindow();
                loginWindow.Show();
                mainWindow.Close();
            }
        }
    }
}
