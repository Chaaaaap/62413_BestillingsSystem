using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    class RibbonViewModel : BaseViewModel
    {
        //private bool _isAdministrator = ApplicationInfo.CurrentUser.IsAdmin;
        public RibbonViewModel(BaseViewModel parent) : base(parent)
        {
        }
    }
}
