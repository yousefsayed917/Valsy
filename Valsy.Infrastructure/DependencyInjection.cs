using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Valsy.Application.Common.Interfaces;
using Valsy.Domain.Common.Abstractions;
using Valsy.Infrastructure.Common.Repositories;
using Valsy.Domain.Products.Repository;
using Valsy.Domain.Orders.Repository;
using Valsy.Domain.Customers.Repository;
using Valsy.Infrastructure.Repositories;

namespace Valsy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;

        services.AddDbContext<ValsyDbContext>(options => options.UseMySQL(connectionString));
        services.AddScoped<DbContext, ValsyDbContext>(sp => sp.GetRequiredService<ValsyDbContext>());
        services.AddScoped<IHilo, HiloGenerator>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
