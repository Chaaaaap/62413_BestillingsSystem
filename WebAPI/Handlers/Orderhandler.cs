using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace WebAPI.Handlers
{
    public class OrderHandler : IDisposable, IOrderHandler
    {
        public void CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(long id)
        {
            throw new NotImplementedException();
        }


        public List<Item> GetAllItems(long id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllUserOrders(long id)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(long i)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(long id, Order order)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
