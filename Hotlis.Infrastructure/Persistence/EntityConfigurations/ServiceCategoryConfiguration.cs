using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.ServiceCategoryAggragate.ValueObjects;
using Hotlis.Domain.ServiceCategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;
public class ServiceCategoryConfiguration:IEntityTypeConfiguration<ServiceCategory>
{
    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ServiceCategoryId")
        .ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(id=>id.Value,
        value=>ServiceCategoryId.Create(value));

        builder.Property(x => x.TenantId).HasColumnName("TenantId")
        .HasMaxLength(10)
        .HasConversion(id => id.Value,
        value => TenantId.Create(value));

        builder.Property(a=>a.Description).HasMaxLength(100);

    }
}