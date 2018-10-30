using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace WebAPI.Handlers
{
    public class ItemHandler : IDisposable, IItemHandler
    {

        private readonly MySqlConnection _conn;

        public ItemHandler()
        {
            var conString = AppSettings.ConnectionString;
            _conn = new MySqlConnection(conString);

        }

        public void CreateItem(Item item)
        {
            
        }

        public void DeleteItem(long id)
        {
            throw new NotImplementedException();
        }

        public List<Item> GetAllItems()
        {
            _conn.Open();
            const string sql = "SELECT * FROM Items;";
            var cmd = new MySqlCommand(sql, _conn);

            var itemList = new List<Item>();
            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                var item = new Item
                {
                    Id = Convert.ToInt64(dataReader["Id"].ToString()),
                    Name = dataReader["Name"].ToString(),
                    Amount = Convert.ToInt32(dataReader["Amount"].ToString()),
                    Price = Convert.ToDouble(dataReader["Price"].ToString())
                };
                itemList.Add(item);
            }
            _conn.Close();
            return itemList;
        }

        public Item GetItem(long id)
        {
            _conn.Open();
            var sql = "SELECT * FROM Items where Id = @Id;"; // + id + ";";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Id", id);

            Item item = null;
            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                item = new Item
                {
                    Id = Convert.ToInt64(dataReader["Id"].ToString()),
                    Name = dataReader["Name"].ToString(),
                    Amount = Convert.ToInt32(dataReader["Amount"].ToString()),
                    Price = Convert.ToDouble(dataReader["Price"].ToString())
                };
            }
            _conn.Close();
            return item;
        }

        public void UpdateItem(long id, Item item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
