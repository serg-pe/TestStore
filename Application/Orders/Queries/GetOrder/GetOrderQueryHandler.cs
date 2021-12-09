using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderVm>
    {
        private readonly IMapper _mapper;
        private readonly IStoreDbContext _dbContext;

        public GetOrderQueryHandler(IMapper mapper, IStoreDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<OrderVm> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order, OrderVm>(await _dbContext.Orders.FirstAsync(order => order.Id == request.OrderId));
            return order;
        }
    }
}
