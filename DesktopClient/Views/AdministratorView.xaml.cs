using DesktopClient.ViewModels;
using System.Windows.Controls;


namespace DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for AdministratorView.xaml
    /// </summary>
    public partial class AdministratorView : UserControl
    {
        public AdministratorView()
        {
            InitializeComponent();
            DataContext = new AdministratorViewModel(null);
        }
    }
}
