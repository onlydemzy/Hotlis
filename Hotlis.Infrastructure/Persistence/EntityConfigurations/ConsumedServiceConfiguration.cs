using Hotlis.Domain.ServiceAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.ConsumedServiceAggregate;
using Hotlis.Domain.ConsumedServiceAggragate.ValueObjects;
using System.Security.Cryptography;
using Microsoft.Identity.Client;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;
public class ConsumedServiceConfiguration:IEntityTypeConfiguration<ConsumedService>
{
    public void Configure(EntityTypeBuilder<ConsumedService> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ConsumedServiceId")
        .ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(id=>id.Value,
        value=>ServiceId.Create(value));

        builder.Property(x => x.TenantId).HasColumnName("TenantId")
        .HasMaxLength(10)
        .HasConversion(id => id.Value,
        value => TenantId.Create(value));

        builder.Property(x => x.Employee).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);
        

        

        ConfigureServiceDetails(builder);
    }

    private void ConfigureServiceDetails(EntityTypeBuilder<ConsumedService> builder)
    {
        builder.OwnsMany(a => a.ConsumedServiceDetails, cs =>
        {
            cs.ToTable("ConsumedServiceDetail");
            cs.WithOwner().HasForeignKey("ConsumedServiceId");
            cs.Property(s => s.Id).HasColumnName("ConsumedServiceDetailId")
            .ValueGeneratedOnAdd()
            .HasConversion(id => id.Value,
            value => ConsumedServiceDetailId.Create(value));

            cs.Property(s => s.TenantId)
            .HasConversion(id => id.Value, value => TenantId.Create(value))
            .HasMaxLength(10);
            cs.HasKey("Id", "ConsumedServiceId");
            cs.Navigation(s => s.Service).IsRequired();
            cs.Property(s => s.Discount).HasColumnType("decimal(18,2)");
            cs.Property(s => s.Tax).HasColumnType("decimal(18,2)");
            cs.OwnsOne(s => s.Price, p =>
            {
                p.Property(pp => pp.Amount).HasColumnName("Price")
                .HasColumnType("decimal(18,2)");
                p.Property(pp => pp.Currency).HasColumnName("Currency")
                .HasMaxLength(3);
            });
            cs.Ignore(a => a.Amount);
        });

    }


}