using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common;
using Common.Models;

namespace DesktopClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ICommand _changePageCommand;

        private BaseViewModel _currentPageViewModel;
        private List<BaseViewModel> _pageViewModels;

        public MainWindowViewModel(BaseViewModel parent) : base(parent)
        {
            PageViewModels.Add(new ItemOverviewViewModel(this));
            PageViewModels.Add(new AdministratorViewModel(this));

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
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
                    OnPropertyChanged("CurrentPageViewModel");
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
    }
}
