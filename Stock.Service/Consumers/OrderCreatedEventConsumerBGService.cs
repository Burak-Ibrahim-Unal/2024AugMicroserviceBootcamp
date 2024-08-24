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

            //create queue
            Channel.QueueDeclare(
                queue: BusConst.StockOrderCreatedEventQueue,
                durable: true, // kuyruk fiziksel olarak rabbitmq ya kaydedilsin mi?
                exclusive: true, // farkli channellardan da bu kuyruğa gelen mesajlar dinlenmeyecekse true
                autoDelete: false, // ilgili servis çöktüğünde,servisin oluşturduğu kuyruk silinsin mi?
                arguments: null);

            Channel.QueueBind(BusConst.StockOrderCreatedEventQueue, BusConst.OrderCreatedEventExchange, routingKey: "", arguments: null);


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
            // autoAck: false olursa kayıtların işlendiğine emin olunur. Böylece otomatil silme olmaz.

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageAsJson = Encoding.UTF8.GetString(body);

                // Process the message
                var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(messageAsJson);

                Console.WriteLine($"Reveived Event: {orderCreatedEvent.OrderId}");

                Channel!.BasicAck(ea.DeliveryTag, false);
            };


            return Task.CompletedTask;
        }

        private Task ProcessMessageAsync(string message)
        {
            // Implement your message processing logic here
            return Task.CompletedTask;
        }
    }
}

