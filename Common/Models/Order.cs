using System.Collections.Generic;

namespace Common.Models
{
    public class Order
    {
        public long Id { get; set; }
        public Dictionary<long, int> ItemsAmount { get; set; }
        public double TotalPrice { get; set; }
        public long UserId { get; set; }
        public Order() { }
    }
}
