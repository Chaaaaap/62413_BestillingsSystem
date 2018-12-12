using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    public class ViewModelLocator
    {
        private ItemOverviewViewModel _itemOverviewViewModel;
        public ItemOverviewViewModel ItemOverviewViewModel => 
            _itemOverviewViewModel ?? (_itemOverviewViewModel = new ItemOverviewViewModel(null));

        //private AdministratorViewModel _administratorViewModel;
        //public AdministratorViewModel AdministratorViewModel => 
        //    _administratorViewModel ?? (_administratorViewModel = new AdministratorViewModel(null));

        //private AdminItemViewModel _adminItemViewModel;
        //public AdminItemViewModel AdminItemViewModel => 
        //    _adminItemViewModel ?? (_adminItemViewModel = new AdminItemViewModel(null));

    }
}
