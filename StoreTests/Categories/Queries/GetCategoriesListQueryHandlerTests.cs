using Application.Categories.Queries.GetCategoriesList;
using AutoMapper;
using Infrastructure.Persistance;
using StoreTests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StoreTests.Categories.Queries
{
    [Collection("QueryCollection")]
    public class GetCategoriesListQueryHandlerTests
    {
        private readonly StoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandlerTests(QueryTestFixture fixture)
        {
            _dbContext = fixture.DbContext;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCategoriesListQueryHandler_Success()
        {
            var handler = new GetCategoriesListQueryHandler(_mapper, _dbContext);

            var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);

            Assert.IsType<CategoriesListVm>(result);
            Assert.Equal(3, result.Categories.Count);
        }
    }
}
