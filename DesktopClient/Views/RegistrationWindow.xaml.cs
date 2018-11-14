using System.Security;
using System.Windows;
using DesktopClient.Helpers;
using DesktopClient.ViewModels;

namespace DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window, IHavePassword
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            this.DataContext = new RegistrationViewModel(null);
        }

        public SecureString Password { get; }
    }
}
