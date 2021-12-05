using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public static class DbInitializer
    {
        public static void Initialize(DbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
