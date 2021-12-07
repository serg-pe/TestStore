using Domain;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;

namespace StoreTests.Common
{
    public static class StoreContextFactory
    {
        public static StoreDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new StoreDbContext(options);
            dbContext.Database.EnsureCreated();
            dbContext.Categories.AddRange(
                new Category
                {
                    Id = Guid.Parse("C31D7F95-BEAB-4788-B95C-59B05426F7F7"),
                    Name = "Category 1",
                    Img = null,
                    Description = "Description for Category 1",
                },
                new Category
                {
                    Id = Guid.Parse("D4E9B2C3-9121-4070-A591-3C171A2A603D"),
                    Name = "Category 2",
                    Img = null,
                    Description = "Description for Category 2",
                },
                new Category
                {
                    Id = Guid.Parse("60F75FCD-D9E2-45D0-B36E-B1BE358CAB53"),
                    Name = "Category 3",
                    Img = null,
                    Description = "Description for Category 3",
                }
                );
            dbContext.SaveChanges();

            return dbContext;
        }

        public static void DestroyDbContext(StoreDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
