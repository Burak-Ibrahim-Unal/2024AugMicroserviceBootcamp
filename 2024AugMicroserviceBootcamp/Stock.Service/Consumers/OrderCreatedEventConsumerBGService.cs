using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Stock.Service.Consumers
{
    public class OrderCreatedEventConsumerBGService(IBus bus) : BackgroundService
    {
        private IModel? Channel { get; set; }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Channel = bus.GetChannel();
            Channel.QueueDeclare(queue: BusConst.StockOrderCreatedEventQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            // queue: Defines the name of the queue. 
            // durable: Specifies whether the queue should survive a broker restart.
            // exclusive : Indicates whether the queue is restricted to only one connection. 
            // autoDelete: Determines whether the queue should be automatically deleted when no consumers are connected.
            // arguments: Allows passing additional optional arguments. 

            Channel.QueueBind(queue: BusConst.StockOrderCreatedEventQueue, exchange: BusConst.OrderCreatedEventExchange, routingKey: "", arguments: null);

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(Channel);

            Channel.BasicConsume(queue: BusConst.StockOrderCreatedEventQueue, autoAck: false, consumer: consumer);
            //autoAck: true > Automatically acknowledges the message upon delivery.
            Channel!.CallbackException += Channel_CallbackException;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

                Console.WriteLine($"Gelen event:{orderCreatedEvent.orderId}");

                Channel!.BasicAck(ea.DeliveryTag, false);
            };

            return Task.CompletedTask;
        }

        private void Channel_CallbackException(object? sender, CallbackExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
