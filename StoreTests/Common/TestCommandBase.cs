using Infrastructure.Persistance;
using System;

namespace StoreTests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly StoreDbContext DbContext;

        public TestCommandBase()
        {
            DbContext = StoreContextFactory.CreateDbContext();
        }

        public void Dispose()
        {
            StoreContextFactory.DestroyDbContext(DbContext);
        }
    }
}
