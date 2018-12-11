using Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    class OrderHistoryViewModel : BaseViewModel
    {
        public OrderHistoryViewModel(BaseViewModel parent) : base(parent)
        {
            Name = "Order history";
            PopulateOrders();
        }

        public string Name { get; set; }

        private Order _selectedOrder;

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (_selectedOrder == value)
                    return;
                _selectedOrder = value;
                if (_selectedOrder == null)
                    ShowItems = false;
                else
                {
                    ShowItems = true;
                    PopulateItem();
                }
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        private ObservableCollection<Order> _orders = new ObservableCollection<Order>();

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                if (_orders == value)
                    return;
                _orders = value;
                OnPropertyChanged(nameof(Orders));
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

        private bool _showItems = false;

        public bool ShowItems
        {
            get => _showItems;
            set
            {
                if (_showItems == value)
                    return;
                _showItems = value;
                OnPropertyChanged(nameof(ShowItems));
            }
        }

        private async void PopulateItem()
        {
            Items.Clear();
            foreach(long id in SelectedOrder.ItemsAmount.Keys)
            {
                Item item = await Service.GetItem(id);
                int amount;
                SelectedOrder.ItemsAmount.TryGetValue(id, out amount);
                item.Amount = amount;
                item.Price = item.Price * amount;
                Items.Add(item);
            }
        }

        private async Task<ObservableCollection<Order>> PopulateOrders()
        {
            List<Order> orderList = await Service.GetAllOrders(ApplicationInfo.CurrentUser.Id);
            Orders.Clear();
            foreach (Order order in orderList)
            {
                Orders.Add(order);
            }
            return Orders;
        }
    }
}
