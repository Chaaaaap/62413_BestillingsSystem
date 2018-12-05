using System;
using System.Collections.Generic;
using Common;
using Common.Models;
using Microsoft.EntityFrameworkCore.Storage;
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

            sql = "INSERT INTO ItemPictures () VALUES (@ItemId, @Picture);";
            cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@ItemId", id);
            cmd.Parameters.AddWithValue("@Picture", item.Picture);

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

            sql = "DELETE FROM ItemPictures Where ItemId = @Id;";
            cmd = new MySqlCommand(sql, _conn);

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
            const string sql = "select Items.Id, Name, Price, Picture, Storage from Items inner join ItemStorage on Items.Id = ItemStorage.ItemId left join ItemPictures on Items.Id = ItemPictures.ItemId;";
            var cmd = new MySqlCommand(sql, _conn);

            var itemList = new List<Item>();
            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                byte[] picture = null;
                if (dataReader["Picture"].GetType().Equals(DBNull.Value))
                    picture = (byte[])dataReader["Picture"];

                var item = new Item
                {
                    Id = Convert.ToInt64(dataReader["Id"].ToString()),
                    Name = dataReader["Name"].ToString(),
                    Amount = Convert.ToInt32(dataReader["Storage"].ToString()),
                    Price = Convert.ToDouble(dataReader["Price"].ToString()),
                    Picture = picture
                };
                itemList.Add(item);
            }
            return itemList;
        }

        public Item GetItem(long id)
        {
            var sql = "select Items.Id, Name, Price, Picture, Storage from Items inner join ItemStorage on Items.Id = ItemStorage.ItemId inner join ItemPictures on Items.Id = ItemPictures.ItemId where Id = @Id;";
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
                    Price = Convert.ToDouble(dataReader["Price"].ToString()),
                    Picture = (byte[]) dataReader["Picture"]
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
            UpdatePicture(id, item.Picture);
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

        private void UpdatePicture(long itemId, byte[] image)
        {
            var sql = "UPDATE ItemPictures SET Picture = @Picture WHERE ItemId = @Id;";

            var cmd = new MySqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@Picture", image);
            cmd.Parameters.AddWithValue("@Id", itemId);

            cmd.ExecuteNonQuery();
        }
    
    }
}
