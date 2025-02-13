using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Order.Commands.Create
{
    public record OrderCreateCommand(string Name, int Quantity, decimal Price) : IRequest<OrderCreateCommandResponse>;
}
