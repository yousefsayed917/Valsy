using Microsoft.EntityFrameworkCore;
using MediatR;
using Valsy.Application.Common.Interfaces;
using Valsy.Domain.Common;
using Valsy.Domain.Customers;
using Valsy.Domain.Orders;
using Valsy.Domain.Products;

namespace Valsy.Infrastructure;

public class ValsyDbContext : DbContext, IApplicationDbContext
{
    private readonly IPublisher? _publisher;

    public ValsyDbContext(
        DbContextOptions<ValsyDbContext> options,
        IPublisher? publisher = null) : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        // Apply Global Query Filter for soft delete automatically to all AuditableEntity types
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
            }
        }
    }

    private static System.Linq.Expressions.LambdaExpression ConvertFilterExpression(Type type)
    {
        var parameter = System.Linq.Expressions.Expression.Parameter(type, "e");
        var property = System.Linq.Expressions.Expression.Property(parameter, nameof(AuditableEntity.IsDeleted));
        var notExpression = System.Linq.Expressions.Expression.Not(property);
        return System.Linq.Expressions.Expression.Lambda(notExpression, parameter);
    }

    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductVariant> ProductVariants { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DispatchDomainEventsAsync(cancellationToken);
        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        if (_publisher is null) return;

        var domainEntities = ChangeTracker
            .Entries<AggregateRoot<Guid>>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        foreach (var entity in domainEntities)
        {
            entity.Entity.ClearDomainEvents();
        }

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }
    }
}
