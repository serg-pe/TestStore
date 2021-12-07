using Application.Interfaces;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                var a = configuration.GetSection("ConnectionStrings");
                options.UseNpgsql(configuration.GetConnectionString("Postgresql"));
            });
            services.AddScoped<IStoreDbContext>(serviceProvider => serviceProvider.GetRequiredService<StoreDbContext>());

            return services;
        }
    }
}
