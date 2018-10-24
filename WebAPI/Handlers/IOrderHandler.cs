using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Handlers
{
    interface IOrderHandler
    {
        Order GetOrder(long i);
        List<Order> GetAllOrder();
        List<Order> GetAllUserOrder(long id);
        List<Item> GetAllItems(long id);
        void CreateOrder(Order order);
        void UpdateOrder(long id, Order order);
        void DeleteOrder(long id);
    }
}
