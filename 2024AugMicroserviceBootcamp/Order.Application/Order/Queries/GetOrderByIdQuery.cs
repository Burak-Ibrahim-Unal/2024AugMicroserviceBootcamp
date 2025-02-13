using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Order.Queries
{
    public record GetOrderByIdQuery(int Id) : IRequest<GetOrderByIdQueryResponse>;
}
