using Application.Products.Queries.GetProductsList;
using AutoMapper;
using Infrastructure.Persistance;
using StoreTests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StoreTests.Products.Queries
{
    [Collection("QueryCollection")]
    public class GetProductsListQueryHandlerTests 
    {
        private readonly StoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductsListQueryHandlerTests(QueryTestFixture fixture) =>
            (_dbContext, _mapper) = (fixture.DbContext, fixture.Mapper);

        [Fact]
        public async Task GetProductsListQueryHandler_Success()
        {
            var handler = new GetProductsListQueryHandler(_mapper, _dbContext);

            var result = await handler.Handle(new GetProductsListQuery(), CancellationToken.None);

            Assert.IsType<ProductsListVm>(result);
            Assert.Equal(6, result.Products.Count);
        }
    }
}
