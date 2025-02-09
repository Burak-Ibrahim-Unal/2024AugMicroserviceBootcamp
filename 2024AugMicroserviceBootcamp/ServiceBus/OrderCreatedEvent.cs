namespace ServiceBus
{
    public record OrderCreatedEvent(int orderId, Dictionary<int, int> stockInfo)
    {

    }

    //eski tarz
    //public class OrderCreatedEvent
    //{
    //    public int OrderId { get; init; }
    //    public Dictionary<int, int> StockInfo { get; init; }

    //    public OrderCreatedEvent(int orderId, Dictionary<int, int> stockInfo)
    //    {
    //        this.OrderId = orderId;
    //        this.StockInfo = stockInfo;
    //    }

    //}


    public class A()
    {
        void X()
        {
            var orderCreatedEvent = new OrderCreatedEvent(1, new Dictionary<int, int>
            {
                {1,10 },
                {2,20},
            });
        }
    }
}
