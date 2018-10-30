using Common;
using System.Collections.Generic;


namespace WebAPI.Handlers
{
    interface IOrderHandler
    {
        Order GetOrder(long i);
        List<Order> GetAllOrders();
        List<Order> GetAllUserOrders(long id);
        List<Item> GetAllItems(long id);
        void CreateOrder(Order order);
        void UpdateOrder(long id, Order order);
        void DeleteOrder(long id);
    }
}
