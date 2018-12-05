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
                CreateItem = false;
                EditItem = true;
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

        private bool _createItem = false;

        public bool CreateItem
        {
            get => _createItem;
            set
            {
                if (_createItem == value)
                    return;
                _createItem = value;
                OnPropertyChanged(nameof(CreateItem));
            }
        }

        private bool _editItem = false;

        public bool EditItem
        {
            get => _editItem;
            set
            {
                if (_editItem == value)
                    return;
                _editItem = value;
                OnPropertyChanged(nameof(EditItem));
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
            Items.Clear();
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
        public ICommand CreateItemCommand
        {
            get;
            private set;
        }
        public ICommand AdminCreateItemCommand
        {
            get;
            private set;
        }

        private void InitializeCommands()
        {
            SaveItemCommand = new CommandHandler(SaveItemChanges);
            CreateItemCommand = new CommandHandler(AdminCreateItem);
            AdminCreateItemCommand = new CommandHandler(CreateItemClicked);
        }

        public async void SaveItemChanges(object sender)
        {
            Item item = new Item
            {
                Name = TmpName ?? SelectedItem.Name,
                Price = TmpPrice ?? SelectedItem.Price,
                Amount = TmpAmount ?? SelectedItem.Amount,
                Id = SelectedItem.Id
            };

            var updatedItem = await Service.UpdateItem(item);
            PopulateItems();

            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(SelectedItem));

            TmpName = null;
            TmpPrice = null;
            TmpAmount = null;
        }

        public async void AdminCreateItem(object sender)
        {
            
            Item item = new Item
            {
                Name = TmpName,
                Price = TmpPrice ?? 0.0,
                Amount = TmpAmount ?? 0
            };

            await Service.CreateItem(item);
            TmpName = null;
            TmpAmount = null;
            TmpPrice = null;
            PopulateItems();
        }

        private void CreateItemClicked(object sender)
        {
            TmpName = null;
            TmpAmount = null;
            TmpPrice = null;
            SelectedItem = null;
            CreateItem = true;
            EditItem = false;

        }
    }
}
