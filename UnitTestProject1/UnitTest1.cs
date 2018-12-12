
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                Username = "TestUser",
                Password  = "Test",
                Email = "test@test.dk",
                IsAdmin = true
            };

            //controller.CreateUser(user);

            User userCheck = controller.GetUserForLogin(user.Username);

            Assert.AreEqual(user.Password, userCheck.Password);
        }

        [TestMethod]
        public void CreateItemTest()
        {
            var controller = new ItemHandler();

            Item item = new Item
            {
                Name = "TestName",
                Price = 25,
                Amount = 10
            };

            var countBefore = controller.GetAllItems().Count;

            controller.CreateItem(item);

            var countAfter = controller.GetAllItems().Count;


            Assert.AreEqual(countBefore, countAfter-1);
        }
    }
}
