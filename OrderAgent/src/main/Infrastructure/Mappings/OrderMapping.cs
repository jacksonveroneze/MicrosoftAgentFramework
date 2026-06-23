using JacksonVeroneze.OrderAgent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Mappings;

internal sealed class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("order", "order");

        builder.HasKey(order => order.Id)
            .HasName("pk_order");

        builder.Property<int>("Version")
            .HasColumnName("version")
            .IsConcurrencyToken()
            .HasDefaultValue(1);
    }
}
