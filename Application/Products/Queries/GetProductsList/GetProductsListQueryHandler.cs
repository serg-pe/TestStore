using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, ProductsListVm>
    {
        private readonly IMapper _mapper;
        private readonly IStoreDbContext _dbContext;

        public GetProductsListQueryHandler(IMapper mapper, IStoreDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ProductsListVm> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<ProductDto> products;
            if (request.CategoryId == null)
                products = await _mapper.ProjectTo<ProductDto>(_dbContext.Products).ToListAsync();
            else
                products = await _mapper.ProjectTo<ProductDto>(_dbContext.Products
                    .Where(product => product.Category.Id == Guid.Parse(request.CategoryId)))
                    .ToListAsync();
            return new ProductsListVm { Products = products };
        }
    }
}
