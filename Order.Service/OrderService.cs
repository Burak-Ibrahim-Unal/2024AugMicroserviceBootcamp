using Microsoft.AspNetCore.Http.HttpResults;
using ServiceBus;

namespace Order.Service;

public class OrderService(IBus bus) : IOrderService
{
    public async Task Create()
    {
        var orderCreatedEvent = new OrderCreatedEvent(
            OrderId: 1, StockInfo: new Dictionary<int, int>()
            {
                {1,2}, {2,3}, {3,4}, {4,5}, {5,6}, {6,7}, {7,8}
            });

        await bus.Send(message: orderCreatedEvent, exchangeName: BusConst.OrderCreatedEventExchange);

    }
}