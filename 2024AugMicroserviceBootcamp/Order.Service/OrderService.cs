using ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service
{
    public class OrderService(IBus bus) : IOrderService
    {
        public async Task Create()
        {
            var orderCreatedEvent = new OrderCreatedEvent(10, new Dictionary<int, int>()
            {
                {1, 1},{2,6}
            });

            string exchangeName = "orderapi.order.created.event.exchange";
            await bus.Send(orderCreatedEvent, BusConst.OrderCreatedEventExchange);

        }
    }
}
