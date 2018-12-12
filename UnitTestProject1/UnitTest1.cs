
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebAPI.Controllers;
using WebAPI.Handlers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateUserTest()
        {
            var controller = new UserHandler();

            User user = new User
            {
                Username = "TestUserTest",
                Password  = "Test",
                Email = "testtra@test.dk",
                IsAdmin = true
            };

            controller.CreateUser(user);

            User userCheck = controller.GetUserForLogin(user.Username);

            Assert.AreEqual(user.Password, userCheck.Password);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var controller = new UserHandler();

            var list = controller.GetAllUsers();

            User user = list[0];

            user.Username = "Ændret";

            var id = user.Id;

            controller.UpdateUser(id, user);

            User userCheck = controller.GetUser(id);

            Assert.AreEqual(userCheck.Username, user.Username);

        }

        [TestMethod]
        public void CreateItemTest()
        {
            var controller = new ItemHandler();

            Item item = new Item
            {
                Name = "TestTest",
                Price = 25,
                Amount = 10
            };

            var countBefore = controller.GetAllItems().Count;

            controller.CreateItem(item);

            var countAfter = controller.GetAllItems().Count;


            Assert.AreEqual(countBefore, countAfter-1);
        }

        [TestMethod]
        public void UpdateitemTest()
        {
            var controller = new ItemHandler();

            var list = controller.GetAllItems();

            Item item = list[0];

            item.Name = "Ændret";

            var id = item.Id;

            controller.UpdateItem(id, item);

            Item itemCheck = controller.GetItem(id);

            Assert.AreEqual(itemCheck.Name, item.Name);

        }

        [TestMethod]
        public void CreateOrderTest()
        {
            var controller = new OrderHandler();

            Dictionary<long, int> itemAmount = new Dictionary<long, int>();
            itemAmount.Add(5, 3);

            var order = new Order()
            {
                ItemsAmount = itemAmount,
                TotalPrice = 25,
                UserId = 2
            };


            var countBefore = controller.GetAllUserOrders(2).Count;

            controller.CreateOrder(order);

            var countAfter = controller.GetAllUserOrders(2).Count;


            Assert.AreEqual(countBefore, countAfter - 1);
        }
    }
}
