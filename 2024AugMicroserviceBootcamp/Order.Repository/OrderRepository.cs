using Order.Application.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        public int CreateOrder(Domain.Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order.Id;
        }
    }
}
