using System;
using System.Collections.Generic;
using Common;
using MySql.Data.MySqlClient;

namespace WebAPI.Handlers
{
    public class ItemHandler : IDisposable, IItemHandler
    {

        private MySqlConnection _conn;

        public ItemHandler(MySqlConnection conn = null)
        {
            _conn = conn ?? new MySqlConnection(AppSettings.ConnectionString);
            _conn.Open();
        }

        public void CreateItem(Item item)
        {
            var sql = "INSERT INTO Items (Name, Price) VALUES(@Name, @Price);";

            var cmd = new MySqlCommand(sql, _conn); ;

            cmd.Parameters.AddWithValue("@Name", item.Name);
            cmd.Parameters.AddWithValue("@Price", item.Price);

            cmd.ExecuteNonQuery();

            var id = cmd.LastInsertedId;

            sql = "INSERT INTO ItemStorage (ItemId, Storage) VALUES (@ItemId, @Amount);";
            cmd = new MySqlCommand(sql, _conn);


            cmd.Parameters.AddWithValue("@ItemId", id);
            cmd.Parameters.AddWithValue("@Amount", item.Amount);

            cmd.ExecuteNonQuery();
        }

        public void UpdateAmount(long id, int amount)
        {
            var sql = "UPDATE ItemStorage SET  Storage = @Amount where ItemId = @Id;";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

        }

        public void DeleteItem(long id)
        {
            var sql = "Delete From ItemStorage Where Id = @Id;";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

            sql = "DELETE FROM Items WHERE Id = @Id;";
            cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _conn.Close();
        }

        public List<Item> GetAllItems()
        {
            const string sql = "select Id, Name, Price, Storage from Items inner join ItemStorage on Id = ItemId;";
            var cmd = new MySqlCommand(sql, _conn);

            var itemList = new List<Item>();
            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                var item = new Item
                {
                    Id = Convert.ToInt64(dataReader["Id"].ToString()),
                    Name = dataReader["Name"].ToString(),
                    Amount = Convert.ToInt32(dataReader["Storage"].ToString()),
                    Price = Convert.ToDouble(dataReader["Price"].ToString())
                };
                itemList.Add(item);
            }
            return itemList;
        }

        public Item GetItem(long id)
        {
            var sql = "select Id, Name, Price, Storage from Items inner join ItemStorage on Id = ItemId; where Id = @Id;";
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
                    Amount = Convert.ToInt32(dataReader["Storage"].ToString()),
                    Price = Convert.ToDouble(dataReader["Price"].ToString())
                };
            }

            return item;
        }

        public void UpdateItem(long id, Item item)
        {
            var sql = "UPDATE Items SET  Name= @Name, Price = @Price where Id = @Id;";

            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", item.Name);
            cmd.Parameters.AddWithValue("@Price", item.Price);

            cmd.ExecuteNonQuery();

            UpdateAmount(id, item.Amount);
        }

        public byte[] GetItemPicture(long itemId)
        {
            var sql = "SELECT Picture FROM ItemPictures WHERE ItemId=@itemId;";

            var cmd = new MySqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@itemId", itemId);

            var dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                return (byte[]) dataReader["Picture"];
            }

            return null;
        }

        public void AddItemPicture(long itemId, byte[] image)
        {
            var sql = "INSERT INTO ItemPictures (Picture, ItemId) VALUES (@pic, @itemId);";

            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@pic", image);
            cmd.Parameters.AddWithValue("@itemId", itemId);

            cmd.ExecuteNonQuery();
        }
    
    }
}
