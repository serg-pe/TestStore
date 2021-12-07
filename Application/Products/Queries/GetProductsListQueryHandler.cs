using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreTests.Products.Queries;

namespace Application.Products.Queries
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, ProductsListVm>
    {
        private readonly IMapper _mapper;
        private readonly IStoreDbContext _dbContext;

        public GetProductsListQueryHandler(IMapper mapper, IStoreDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ProductsListVm> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var products = await _mapper.ProjectTo<ProductDto>(_dbContext.Products).ToListAsync();
            return new ProductsListVm { Products = products };
        }
    }
}
