using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using DesktopClient.Events;

namespace DesktopClient.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        public readonly ObservableCollection<Item> OrderItems = new ObservableCollection<Item>();


        private void AddItemToCart(ItemAddedEventArgs e)
        {
            OrderItems.Add(e.ItemAdded);
        }

        public ShoppingCartViewModel(BaseViewModel parent) : base(parent)
        {
            EventAggregator.GetEvent<ItemAddedEvent>().Subscribe(AddItemToCart);
        }
    }
}
