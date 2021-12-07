using Application.Common.Mapping;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Persistance;
using System;
using Xunit;

namespace StoreTests.Common
{
    public class QueryTestFixture : IDisposable
    {

        public StoreDbContext DbContext;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            DbContext = StoreContextFactory.CreateDbContext();
            var configurationProvider = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new AssemblyMappingProfile(typeof(IStoreDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            StoreContextFactory.DestroyDbContext(DbContext);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
