using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    class Order
    {
        public long Id { get; set; }
        public List<Item> items { get; set; }
        public double totalPrice { get; set; }
        public long userId { get; set; }
        public Order() { }
    }
}
