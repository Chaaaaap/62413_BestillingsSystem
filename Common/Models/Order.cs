﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Order
    {
        public long Id { get; set; }
        public List<Item> Items { get; set; }
        public double TotalPrice { get; set; }
        public long UserId { get; set; }
        public Order() { }
    }
}
