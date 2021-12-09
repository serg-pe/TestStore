using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, OrdersListVm>
    {
        private readonly IStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IStoreDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<OrdersListVm> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _mapper.ProjectTo<OrderDto>(_dbContext.Orders).ToListAsync();
            return new OrdersListVm { Orders = orders };
        }
    }
}
