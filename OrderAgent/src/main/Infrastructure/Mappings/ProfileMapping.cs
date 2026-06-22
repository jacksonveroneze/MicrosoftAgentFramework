using JacksonVeroneze.OrderAgent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Mappings;

internal sealed class ProfileMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("profile", "profile");

        builder.HasKey(profile => profile.Id)
            .HasName("pk_profile");

        builder.Property<int>("Version")
            .HasColumnName("version")
            .IsConcurrencyToken()
            .HasDefaultValue(1);
    }
}
