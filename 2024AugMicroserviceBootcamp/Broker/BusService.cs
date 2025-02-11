using Order.Application;
using Order.Application.Order.CreateOrderUseCase;

namespace Broker
{
    public class BusService : IBusService
    {
        public Task PublishAsync(OrderCreatedEvent orderCreatedEvent)
        {
            Console.WriteLine("Message is sent (RabbitMq)");

            return Task.CompletedTask;
        }
    }
}
