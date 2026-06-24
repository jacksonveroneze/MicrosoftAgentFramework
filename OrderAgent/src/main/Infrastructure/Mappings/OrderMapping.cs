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

        builder.Property(order => order.UserId)
            .IsRequired();

        builder.Property(order => order.AccountId)
            .IsRequired();

        builder.Property(order => order.AssetTicker)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(order => order.Side)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(order => order.OrderType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(order => order.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(order => order.Quantity)
            .HasPrecision(18, 8)
            .IsRequired();

        builder.Property(order => order.Price)
            .HasPrecision(18, 8)
            .IsRequired();

        builder.Property(order => order.FilledQuantity)
            .HasPrecision(18, 8)
            .IsRequired();

        builder.Property(order => order.RemainingQuantity)
            .HasPrecision(18, 8)
            .IsRequired();

        builder.Property(order => order.AveragePrice)
            .HasPrecision(18, 8)
            .IsRequired();

        builder.Property(order => order.TotalAmount)
            .HasPrecision(18, 8)
            .IsRequired();

        builder.Property(order => order.RejectionReason)
            .HasMaxLength(500);

        builder.Property(order => order.CreatedAtUtc)
            .IsRequired();

        builder.Property(order => order.UpdatedAtUtc);

        builder.Property(order => order.ExecutedAtUtc);

        builder.Property(order => order.CancelledAtUtc);

        builder.HasIndex(order => new { order.UserId, order.AssetTicker })
            .HasDatabaseName("ix_order_user_id_asset_ticker");
    }
}
