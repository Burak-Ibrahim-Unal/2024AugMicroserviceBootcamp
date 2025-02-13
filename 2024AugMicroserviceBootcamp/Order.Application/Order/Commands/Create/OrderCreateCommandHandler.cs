using MediatR;
using Order.Application.Order.CreateOrderUseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Order.Commands.Create
{
    public class OrderCreateCommandHandler(IOrderRepository orderRepository, IBusService busService) : IRequestHandler<OrderCreateCommand, OrderCreateCommandResponse>
    {
        public async Task<OrderCreateCommandResponse> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var order = new Domain.Order
            {
                Name = request.Name,
                Quantity = request.Quantity,
                Price = request.Price,
            };

            var orderId = orderRepository.CreateOrder(order);
            await busService.PublishAsync(new OrderCreatedEvent(orderId, order.Quantity));

            return new OrderCreateCommandResponse(orderId);
        }
    }
}
