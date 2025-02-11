using MassTransit;
using ServiceBus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Service.Consumers
{
    public class OrderCreatedEventConsumer(IPublishEndpoint publishEndpoint) : IConsumer<OrderCreatedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            #region TEST
            //var hasStock = true;
            //if (hasStock) 
            //{
            //    // Send payment event
            //}
            //else
            //{
            //    // send there is no stock event
            //}


            //Console.WriteLine("Consumer started to read data...");
            //throw new DBConcurrencyException(); 
            #endregion

            Console.WriteLine($"(MassTransit) Gelen event: {context.Message.orderId}");

            return Task.CompletedTask;
        }
    }
}
