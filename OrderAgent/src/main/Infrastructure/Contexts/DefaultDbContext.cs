using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.OrderAgent.Domain.Entities;
using JacksonVeroneze.OrderAgent.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class DefaultDbContext(
    DbContextOptions<DefaultDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Constants.SchemaName);

        modelBuilder.ApplyConfiguration(new ProfileMapping());

        modelBuilder.Entity<Order>()
            .HasQueryFilter(field => field.DeletedAt == null);
        
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<DomainEvent>();
    }
}
