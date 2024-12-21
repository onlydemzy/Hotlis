using Hotlis.Domain.Entities;
using Hotlis.Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations.UserManagement;
public class TenantConfiguration:IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenant");
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id).ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(a=>a.Value,
        value=>TenantId.Create(value));
        builder.Property(x=>x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x=>x.TenantCode).HasMaxLength(10).IsRequired();
        builder.Property(x=>x.Email).HasMaxLength(100).IsRequired(false);
        builder.Property(x=>x.Phone1).HasMaxLength(20).IsRequired();
        builder.Property(x=>x.Phone2).HasMaxLength(20).IsRequired(false);
        builder.Property(x=>x.Address).HasMaxLength(100).IsRequired();
        builder.Property(x=>x.City).HasMaxLength(50).IsRequired();
        builder.Property(x=>x.State).HasMaxLength(50).IsRequired();
        builder.Property(x=>x.Country).HasMaxLength(50).IsRequired();
        builder.Property(x=>x.Status).HasMaxLength(30).IsRequired();
    }
}