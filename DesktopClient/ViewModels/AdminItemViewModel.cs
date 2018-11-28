using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common;

namespace DesktopClient.ViewModels
{
    class AdminItemViewModel : BaseViewModel
    {
        public AdminItemViewModel(BaseViewModel parent) : base(parent)
        {
            InitializeCommands();
            PopulateItems();
        }

        private Item _selectedItem;

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value)
                    return;
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private string _tmpName;

        public string TmpName
        {
            get => _tmpName;
            set
            {
                if (_tmpName == value)
                    return;
                _tmpName = value;
                OnPropertyChanged(nameof(TmpName));
            }
        }

        private double? _tmpPrice;

        public double? TmpPrice
        {
            get => _tmpPrice;
            set
            {
                if (_tmpPrice == value)
                    return;
                _tmpPrice = value;
                OnPropertyChanged(nameof(TmpPrice));
            }
        }

        private int? _tmpAmount;

        public int? TmpAmount
        {
            get => _tmpAmount;
            set
            {
                if (_tmpAmount == value)
                    return;
                _tmpAmount = value;
                OnPropertyChanged(nameof(TmpAmount));
            }
        }

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

        private async void PopulateItems()
        {
            List<Item> itemList = await Service.GetAllItems();

            foreach (Item item in itemList)
            {
                Items.Add(item);
            }
        }

        public ICommand SaveItemCommand
        {
            get;
            private set;
        }

        private void InitializeCommands()
        {
            SaveItemCommand = new CommandHandler(SaveItemChanges);
        }

        public void SaveItemChanges(object sender)
        {
            Item item = new Item
            {
                Name = TmpName ?? SelectedItem.Name,
                Price = TmpPrice ?? SelectedItem.Price,
                Amount = TmpAmount ?? SelectedItem.Amount,
                Id = SelectedItem.Id
            };

            var updatedItem = Service.UpdateItem(item).Result;

            var tmpItem = Items.Where(u => u.Id == updatedItem.Id).FirstOrDefault();
            if (tmpItem != null)
            {
                Items.Remove(tmpItem);
            }

            Items.Add(updatedItem);
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(SelectedItem));

            TmpName = null;
            TmpPrice = null;
            TmpAmount = null;
        }
    }
}
