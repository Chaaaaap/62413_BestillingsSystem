using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class OrdersItem
    {
        public long OrderId { get; set; }
        public long ItemsId { get; set; }
        public int Amount { get; set; }
        public OrdersItem() { }
    }
}
