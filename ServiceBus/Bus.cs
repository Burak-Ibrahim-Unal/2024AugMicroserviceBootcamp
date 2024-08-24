﻿using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ServiceBus
{
    public class Bus(IOptions<BusOption> busOptions) : IBus
    {
        public Task Send<T>(T message, string exchangeName) where T : class
        {
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(busOptions.Value.Url)
            };

            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ConfirmSelect();
            // create exchange
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange: exchangeName,
                                routingKey: "",
                                basicProperties: null,
                                body: body);

            channel.WaitForConfirms(TimeSpan.FromMinutes(1));
            return Task.CompletedTask;

        }
    }
}