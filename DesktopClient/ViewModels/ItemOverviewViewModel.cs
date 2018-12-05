using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Models;

namespace DesktopClient.ViewModels
{
    public class ItemOverviewViewModel : BaseViewModel
    {
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();

        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                if (_items == value)
                    return;
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public ItemOverviewViewModel(BaseViewModel parent) : base(parent)
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
