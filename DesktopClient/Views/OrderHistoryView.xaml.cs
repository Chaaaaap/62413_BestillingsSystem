using DesktopClient.ViewModels;
using System.Windows.Controls;


namespace DesktopClient.Views
{
    /// <summary>
    /// </summary>
    public partial class OrderHistoryView : UserControl
    {
        public OrderHistoryView()
        {
            InitializeComponent();
            DataContext = new OrderHistoryViewModel(null);
        }
    }
}
