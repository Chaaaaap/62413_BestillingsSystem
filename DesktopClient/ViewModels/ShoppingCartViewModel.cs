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
        public ObservableCollection<Item> OrderItems { get; set; }


        public void AddItemToCart(ItemAddedEventArgs e)
        {
            
        }

        public ShoppingCartViewModel(BaseViewModel parent) : base(parent)
        {
            EventAggregator.GetEvent<ItemAddedEvent>().Subscribe(AddItemToCart);
        }
    }
}
