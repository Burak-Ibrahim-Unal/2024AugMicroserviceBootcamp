using MassTransit;
using Oder.API.Services;
using ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service.Services
{
    public class OrderService(ServiceBus.IBus bus, IPublishEndpoint publishEndpoint, StockService stockService) : IOrderService
    {
        public async Task Create()
        {
            var orderCreatedEvent = new OrderCreatedEvent(10, new Dictionary<int, int>()
            {
                {1, 1},{2,6}
            });

            //await bus.Send(orderCreatedEvent, BusConst.OrderCreatedEventExchange);

            var result = await stockService.CheckStockAsync(1, 5);

            CancellationTokenSource cancellationTokenSource = new();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(60));
            await publishEndpoint.Publish(
                orderCreatedEvent,
                pipeline =>
                {
                    pipeline.SetAwaitAck(true); //throw exception if data is not processed / saved
                    pipeline.Durable = true; // save data to disk for protection of restarting
                    pipeline.TimeToLive = TimeSpan.FromSeconds(360); // The message lifetime is 360 seconds.
                },
                cancellationTokenSource.Token
            );
        }
    }
}
