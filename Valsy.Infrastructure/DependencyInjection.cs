using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Valsy.Application.Common.Interfaces;
using Valsy.Domain.Common.Abstractions;
using Valsy.Infrastructure.Common.Repositories;

namespace Valsy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ValsyDbContext>(options => options.UseMySQL(connectionString));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ValsyDbContext>());
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
