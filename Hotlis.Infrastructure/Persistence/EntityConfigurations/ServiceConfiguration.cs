using Hotlis.Domain.ServiceAggregate;
using Hotlis.Domain.ServiceAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.ServiceCategoryAggragate.ValueObjects;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;
public class ServiceConfiguration:IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ServiceId")
        .ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(id=>id.Value,
        value=>ServiceId.Create(value));

        builder.Property(x => x.TenantId).HasColumnName("TenantId")
        .HasMaxLength(10)
        .HasConversion(id => id.Value,
        value => TenantId.Create(value));

        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(x => x.Amount).HasColumnName("Price")
            .HasColumnType("decimal(18,2)");
            price.Property(x => x.Currency).HasColumnName("Currency")
            .HasMaxLength(3);
        });

        builder.Property(s=>s.ServiceCategoryId).HasColumnName("ServiceCategoryId")
        .HasConversion(id=>id.Value,
        value=>ServiceCategoryId.Create(value))
        .HasMaxLength(60);

        builder.Property(s=>s.TaxRate).HasColumnType("decimal(18,2)");

        builder.ToTable("Service");
    }
}