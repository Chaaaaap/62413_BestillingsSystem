using Common;
using System.Collections.Generic;


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
