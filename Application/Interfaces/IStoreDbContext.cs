using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IStoreDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
