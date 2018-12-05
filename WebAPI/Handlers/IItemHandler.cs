using System.Collections.Generic;
using Common;
using Common.Models;
using MySql.Data.MySqlClient;

namespace WebAPI.Handlers
{
    interface IItemHandler
    {
        Item GetItem(long i);
        List<Item> GetAllItems();
        void CreateItem(Item item);
        void UpdateItem(long id, Item item);
        void DeleteItem(long id);
        void UpdateAmount(long id, int amount);
        byte[] GetItemPicture(long id);
        void AddItemPicture(long itemId, byte[] image);
    }
}
