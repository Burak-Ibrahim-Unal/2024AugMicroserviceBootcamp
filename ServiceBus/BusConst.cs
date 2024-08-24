using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public static class BusConst
    {
        public const string OrderCreatedEventExchange = "order.api.order.created.event.exchange";
        public const string StockOrderCreatedEventQueue = "stock.api.order.created.event.exchange";
    }
}
