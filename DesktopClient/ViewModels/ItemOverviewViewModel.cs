using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Common.Models;
using DesktopClient.Events;

namespace DesktopClient.ViewModels
{
    public class ItemOverviewViewModel : BaseViewModel
    {
        #region UI Properties

        private ObservableCollection<Item> _selectedItems = new ObservableCollection<Item>();
        public ObservableCollection<Item> SelectedItems
        {
            get => _selectedItems;
            set
            {
                if (_selectedItems == value)
                    return;
                _selectedItems = value;
                OnPropertyChanged(nameof(SelectedItems));
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

        private string _tmpSearch;

        public string TmpSearch
        {
            get => _tmpSearch;
            set
            {
                if (value == _tmpSearch)
                {
                    return;
                }

                _tmpSearch = value;
                OnPropertyChanged(TmpSearch);
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name)
                    return;
                _name = value;
                OnPropertyChanged(Name);
            }
        }

        private double _totalPrice;

        public double TotalPrice
        {
            get => _totalPrice;
            set
            {
                if (value == _totalPrice)
                    return;
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        #endregion

        public ItemOverviewViewModel(BaseViewModel parent) : base(parent)
        {
            Name = "Shopping";
            InitializeCommands();
            PopulateItems();
        }

        public ICommand SearchItemCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }
        public ICommand AddItemToCartCommand { get; set; }
        public ICommand PurchaseCommand { get; set; }

        private void InitializeCommands()
        {
            SearchItemCommand = new CommandHandler(SearchItems);
            ClearSearchCommand = new CommandHandler(ClearSearch);
            AddItemToCartCommand = new CommandHandler(AddItemClicked);
            PurchaseCommand = new CommandHandler(Purchase);
        }

        private async Task<ObservableCollection<Item>> PopulateItems()
        {
            var itemList = await Service.GetAllItems();

            Items.Clear();

            foreach (var item in itemList)
            {
                Items.Add(item);
            }

            return Items;
        }

        private async void SearchItems(object sender)
        {
            await PopulateItems();
            var temp = Items.Select(x => x).Where(x => x.Name.ToLower().Contains(TmpSearch.ToLower()));

            List<Item> itemTemp = new List<Item>();

            foreach (Item item in temp)
            {
                itemTemp.Add(item);
            }

            Items.Clear();

            foreach (Item item in itemTemp)
            {
                Items.Add(item);
            }
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(TmpSearch));
        }

        public async void ClearSearch(object sender)
        {
            await PopulateItems();
            TmpSearch = "";
        }

        public void AddItemClicked(object sender)
        {
            TotalPrice += SelectedItem.Price;
            SelectedItems.Add(SelectedItem);
        }

        public async void Purchase(object sender)
        {
            var itemAmount = new Dictionary<long, int>();
            foreach (var items in SelectedItems)
            {
                if (itemAmount.ContainsKey(items.Id))
                {
                    itemAmount[items.Id] += 1;
                }
                else
                {
                    itemAmount.Add(items.Id, 1);
                }
            }

            var order = new Order()
            {
                ItemsAmount = itemAmount,
                TotalPrice = TotalPrice,
                UserId = ApplicationInfo.CurrentUser.Id
            };
            await Service.CreateOrder(order);

            SelectedItems.Clear();
            MessageBox.Show(
                "The order has been created. \nYou can now see it under \"Order history\"", 
                "Confirmation", 
                MessageBoxButton.OK);
        }
    }
}
