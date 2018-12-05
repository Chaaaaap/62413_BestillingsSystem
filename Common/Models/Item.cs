using System;

namespace Common.Models
{
    public class Item
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public byte[] Picture { get; set; }
        public Item() { }
    }
}
