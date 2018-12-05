using Common;
using System.Collections.Generic;
using Common.Models;


namespace WebAPI.Handlers
{
    interface IOrderHandler
    {
        Order GetOrder(long id);
        List<Order> GetAllOrders();
        List<Order> GetAllUserOrders(long id);
        void CreateOrder(Order order);
        void DeleteOrder(long id);
    }
}
