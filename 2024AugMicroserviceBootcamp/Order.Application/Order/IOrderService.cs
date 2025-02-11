using Order.Application.Order.CreateOrderUseCase;

namespace Order.Application.Order
{
    public interface IOrderService
    {
        Task<OrderCreateResponse> CreateOrder(OrderCreateRequest request);

    }
}