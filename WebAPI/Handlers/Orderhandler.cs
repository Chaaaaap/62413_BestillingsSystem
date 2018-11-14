﻿using System;
using System.Collections.Generic;
using Common;
using MySql.Data.MySqlClient;

namespace WebAPI.Handlers
{
    public class OrderHandler : IDisposable, IOrderHandler
    {
        private readonly MySqlConnection _conn;

        public OrderHandler(MySqlConnection conn = null)
        {
            _conn = conn ?? new MySqlConnection(AppSettings.ConnectionString);
            _conn.Open();
        }

        public void CreateOrder(Order order)
        {

            ItemHandler itemHandler = new ItemHandler(_conn);

            var sql = "INSERT INTO Orders (UserId, TotalPrice) VALUES (@UserId, @TotalPrice);";

            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@UserId", order.UserId);
            cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);

            cmd.ExecuteNonQuery();

            foreach (var element in order.ItemsAmount)
            {
                sql = "INSERT INTO OrdersItem (OrderId, ItemId, Amount) VALUES (@OrderId, @ItemId, @Amount);";
                cmd = new MySqlCommand(sql, _conn);

                cmd.Parameters.AddWithValue("@OrderId", Convert.ToInt64(cmd.LastInsertedId));
                cmd.Parameters.AddWithValue("@ItemId", element.Key.Id);
                cmd.Parameters.AddWithValue("@Amount", element.Value);


                itemHandler.updateAmount(element.Key.Id, -element.Value);
            }
        }

        public void DeleteOrder(long id)
        {

            ItemHandler itemHander = new ItemHandler(_conn);

            Order order = GetOrder(id);

            var sql = "DELETE * FROM OrdersItem WHERE OrderId = @id";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

            sql = "DELETE * FROM Orders WHERE Id = @id";
            cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

            foreach (var element in order.ItemsAmount){
                itemHander.updateAmount(element.Key.Id, element.Value);
            }

        }

        public List<Order> GetAllOrders()
        {
            const string sql = @"select UserId, TotalPrice, OrderId, ItemsId, Amount, Storage, Name, Price from Orders inner join OrdersItem on Orders.Id = OrderId inner join Items on ItemsId = Items.Id inner join ItemStorage on ItemId = Items.Id;";
            var cmd = new MySqlCommand(sql, _conn);

            Order order = null;
            var itemAmount = new Dictionary<Item, int>();
            var orderList = new List<Order>();
            long orderId = -1;

            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                if (orderId != Convert.ToInt64(dataReader["OrderId"].ToString()))
                {
                    orderId = Convert.ToInt64(dataReader["OrderId"].ToString());
                    order.ItemsAmount = itemAmount;
                    orderList.Add(order);
                    itemAmount.Clear();
                };

                Item item = new Item
                {
                    Id = Convert.ToInt64(dataReader["ItemsId"].ToString()),
                    Amount = Convert.ToInt32(dataReader["Storage"].ToString()),
                    Name = dataReader["Name"].ToString(),
                    Price = Convert.ToDouble(dataReader["Price"].ToString())
                };

                itemAmount.Add(item, Convert.ToInt32(dataReader["Amount"].ToString()));
                order = new Order
                {
                    Id = Convert.ToInt64(dataReader["OrderId"].ToString()),
                    UserId = Convert.ToInt64(dataReader["UserId"].ToString()),
                    TotalPrice = Convert.ToDouble(dataReader["TotalPrice"].ToString())
                };
            }
            order.ItemsAmount = itemAmount;
            orderList.Add(order);
            return orderList;

        }

        public List<Order> GetAllUserOrders(long id)
        {
            const string sql = "select UserId, TotalPrice, OrderId, ItemsId, Amount, Storage, Name, Price from Orders inner join OrdersItem on Orders.Id = OrderId inner join Items on ItemsId = Items.Id inner join ItemStorage on ItemId = Items.Id where UserId = id;" ;
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("Id", id);

            Order order = null;
            var itemAmount = new Dictionary<Item, int>();
            var orderList = new List<Order>();
            long orderId = -1;

            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                if (orderId != Convert.ToInt64(dataReader["OrderId"].ToString()))
                {
                    orderId = Convert.ToInt64(dataReader["OrderId"].ToString());
                    order.ItemsAmount = itemAmount;
                    orderList.Add(order);
                    itemAmount.Clear();
                };

                Item item = new Item
                {
                    Id = Convert.ToInt64(dataReader["ItemsId"].ToString()),
                    Amount = Convert.ToInt32(dataReader["Storage"].ToString()),
                    Name = dataReader["Name"].ToString(),
                    Price = Convert.ToDouble(dataReader["Price"].ToString())
                };

                itemAmount.Add(item, Convert.ToInt32(dataReader["Amount"].ToString()));
                order = new Order
                {
                    Id = Convert.ToInt64(dataReader["OrderId"].ToString()),
                    UserId = Convert.ToInt64(dataReader["UserId"].ToString()),
                    TotalPrice = Convert.ToDouble(dataReader["TotalPrice"].ToString())
                };
            }
            order.ItemsAmount = itemAmount;
            orderList.Add(order);
            return orderList;
        }

        public Order GetOrder(long id)
        {
            var sql = "select UserId, TotalPrice, OrderId, ItemsId, Amount, Storage, Name, Price from Orders inner join OrdersItem on Orders.Id = OrderId inner join Items on ItemsId = Items.Id inner join ItemStorage on ItemId = Items.Id; WHERE Orders.Id = @Id;";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Id", id);

            Order order = null;
            var itemAmount = new Dictionary<Item, int>();

            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                Item item = new Item
                {
                    Id = Convert.ToInt64(dataReader["ItemsId"].ToString()),
                    Amount = Convert.ToInt32(dataReader["Storage"].ToString()),
                    Name = dataReader["Name"].ToString(),
                    Price = Convert.ToDouble(dataReader["Price"].ToString())

                };

                itemAmount.Add(item, Convert.ToInt32(dataReader["Amount"].ToString()));
                
                order = new Order
                {
                    Id = Convert.ToInt64(dataReader["OrderId"].ToString()),
                    UserId = Convert.ToInt64(dataReader["UserId"].ToString()),
                    TotalPrice = Convert.ToDouble(dataReader["TotalPrice"].ToString())
                };
            }
            dataReader.Close();
            order.ItemsAmount = itemAmount;
            return order;
        }

        public void Dispose()
        {
            _conn.Close();
        }
    }
}
