using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductVm>
    {
        private readonly IMapper _mapper;
        private readonly IStoreDbContext _dbContext;

        public GetProductQueryHandler(IMapper mapper, IStoreDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ProductVm> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _mapper
                .ProjectTo<ProductVm>(_dbContext.Products.Where(product => product.Id == Guid.Parse(request.ProductId)))
                .FirstOrDefaultAsync(CancellationToken.None);
            return product;
        }
    }
}
