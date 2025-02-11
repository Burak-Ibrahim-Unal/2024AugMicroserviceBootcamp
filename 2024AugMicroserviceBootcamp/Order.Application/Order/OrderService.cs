using Order.Application.Order.CreateOrderUseCase;

namespace Order.Application.Order
{
    public class OrderService(IOrderRepository orderRepository,IBusService busService) : IOrderService
    {
        public async Task<OrderCreateResponse> CreateOrder(OrderCreateRequest request)
        {
            var order = new Domain.Order
            {
                Name = request.Name,
                Quantity = request.Quantity,
                Price = request.Price,
            };

            var orderId = orderRepository.CreateOrder(order);
            await busService.PublishAsync(new OrderCreatedEvent(orderId, order.Quantity));

            return new OrderCreateResponse(orderId);
        }
    }
}