﻿using System.Collections.Generic;
using Common;

namespace WebAPI.Handlers
{
    interface IItemHandler
    {
        Item GetItem(long i);
        List<Item> GetAllitems();
        void CreateItem(Item item);
        void UpdateItem(long id, Item item);
        void DeleteItem(long id);
    }
}
