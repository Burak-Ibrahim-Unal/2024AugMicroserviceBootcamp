using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBus
{
    public class Bus(IOptions<BusOption> busOptions) : IBus
    {
        public  Task Send<T>(T message, string exchangeName) where T : class
        {
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(busOptions.Value.Url)
            };

            using var connection =  connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ConfirmSelect();

            //create exchange
            //string exchange = "orderapi.order.created.event.exchange";
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

            var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            var properties = channel.CreateBasicProperties();


            channel.BasicPublish(
                                exchange: exchangeName,
                                routingKey: "", // Fanout Exchange kullanıyorsan routing key boş olmalı
                                basicProperties: null, // Boş bir BasicProperties nesnesi oluştur
                                body: messageBody
            );
            channel.WaitForConfirms(TimeSpan.FromMinutes(1));

            return Task.CompletedTask; 
        }
    }
}
