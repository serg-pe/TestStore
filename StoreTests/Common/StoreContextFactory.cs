using Domain;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

            var categories = dbContext.Categories.ToArray();
            dbContext.Products.AddRange(
                new Product
                {
                    Id = Guid.Parse("92AD3D08-B4B2-4465-8CF9-97E8263792A6"),
                    Category = categories[0],
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Img = null,
                    Pieces = 20,
                    Price = decimal.Parse("20,00"),
                },
                new Product
                {
                    Id = Guid.Parse("45F4F4EB-0B01-4E21-9BAE-33010986DAD6"),
                    Category = categories[0],
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    Img = null,
                    Pieces = 11,
                    Price = decimal.Parse("200,00"),
                },
                new Product
                {
                    Id = Guid.Parse("66726B97-4BB6-495E-B597-D1E77D3B3886"),
                    Category = categories[0],
                    Name = "Product 3",
                    Description = "Description for Product 3",
                    Img = null,
                    Pieces = 0,
                    Price = decimal.Parse("1499,99"),
                },
                new Product
                {
                    Id = Guid.Parse("1AA2CE80-FFE3-462A-9951-9D9AD49886E1"),
                    Category = categories[1],
                    Name = "Product 4",
                    Description = "Description for Product 4",
                    Img = null,
                    Pieces = 1001,
                    Price = decimal.Parse("1499,99"),
                },
                new Product
                {
                    Id = Guid.Parse("0BD2B543-D3FD-46BA-90B5-EB2141C51464"),
                    Category = categories[1],
                    Name = "Product 5",
                    Description = "Description for Product 5",
                    Img = null,
                    Pieces = 238,
                    Price = decimal.Parse("3,99"),
                },
                new Product
                {
                    Id = Guid.Parse("C03FA2BB-D473-4AF5-A17C-CAF8087E2D1D"),
                    Category = categories[2],
                    Name = "Product 6",
                    Description = "Description for Product 6",
                    Img = null,
                    Pieces = 5,
                    Price = decimal.Parse("29,1"),
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
