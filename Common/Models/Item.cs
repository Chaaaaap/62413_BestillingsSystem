﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Item
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public Item() { }
    }
}
