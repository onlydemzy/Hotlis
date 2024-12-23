using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.RoomCategoryAggragate.ValueObjects;
using Hotlis.Domain.RoomRatesAggragate;
using Hotlis.Domain.RoomRatesAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;
public class RoomRatesConfiguration:IEntityTypeConfiguration<RoomRates>
{
    public void Configure(EntityTypeBuilder<RoomRates> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
        .HasColumnName("RoomRatesId")
        .ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(d=>d.Value,
        value=>RoomRatesId.Create(value));

        builder.Property(s=>s.RoomCategoryId)
        .HasConversion(d => d.Value,
        value => RoomCategoryId.Create(value));


        builder.Property(x => x.Plan).HasMaxLength(20);
        builder.OwnsOne(x=>x.Money, a=>
        {
            a.Property(x=>x.Currency).HasMaxLength(3).IsRequired();
            a.Property(x=>x.Amount).HasColumnType("decimal(18,2)");
        });

        builder.Property(x => x.TenantId).HasColumnName("TenantId")
        .HasMaxLength(10)
        .HasConversion(id => id.Value,
        value => TenantId.Create(value));
    }
}