using Hotlis.Domain.RoomRatesAggragate;
using Hotlis.Domain.RoomRatesAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        builder.Property(x => x.Plan).HasMaxLength(20);
        builder.OwnsOne(x=>x.Price, a=>
        {
            a.Property(x=>x.Currency).HasMaxLength(3).IsRequired();
            a.Property(x=>x.Amount).HasColumnType("decimal(18,2)");
        });
    }
}