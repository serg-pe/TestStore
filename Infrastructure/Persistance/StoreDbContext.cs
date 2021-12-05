using Application.Interfaces;
using Domain;
using Infrastructure.Persistance.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class StoreDbContext : DbContext, IStoreDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }

        public StoreDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration())
                .ApplyConfiguration(new CategoryConfiguration())
                .ApplyConfiguration(new OrderConfiguration())
                .ApplyConfiguration(new ClientConfiguration());
        }
    }
}
