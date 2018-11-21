using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DesktopClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items = new ObservableCollection<Item>();
        public MainWindowViewModel(BaseViewModel parent) : base(parent)
        {
            InitializeCommands();
            PopulateItems();
        }

        private void InitializeCommands()
        {
            
        }

        private async void PopulateItems()
        {
            List<Item> itemList = await Service.GetAllItems();

            foreach (var item in itemList)
            {
                Items.Add(item);
            }
        }
    }
}
