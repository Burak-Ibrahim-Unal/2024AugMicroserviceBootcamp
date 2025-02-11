namespace Order.Application.Order
{
    public interface IOrderRepository
    {
        int CreateOrder(Domain.Order order);
    }
}