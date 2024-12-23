using Hotlis.Domain.BookingAggragate;
using Hotlis.Domain.BookingAggragate.ValueObjects;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.GuestAggragate.ValueObjects;
using Hotlis.Domain.RoomAggragate;
using Hotlis.Domain.RoomAggragate.ValueObjects;
using Hotlis.Domain.RoomCategoryAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;
public class BookingConfiguration:IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Booking");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
        .HasMaxLength(60)
        .HasColumnName("BookingId")
        .ValueGeneratedNever()
        .HasConversion(s=>s.Value,
            value=>BookingId.Create(value));
           
        
        builder.Property(x => x.Status).HasMaxLength(30);
        builder.Property(x=>x.BookedBy).HasMaxLength(70);
        builder.Property(x=>x.Segment).HasMaxLength(50);

        builder.Property(a => a.RoomId)
        .HasConversion(id => id != null ? id.Value : null,
        value => value != null ? (RoomId?)RoomId.Create(value) : null)
        .IsRequired(false)
        .HasMaxLength(60);

        builder.Property(a=>a.RoomCategoryId)
        .HasConversion(s=>s.Value,
        value=>RoomCategoryId.Create(value))
        .HasMaxLength(60)
        .HasColumnName("RoomCategoryId");

        builder.Property(s => s.GuestId)
        .HasConversion(d => d.Value,
        value => GuestId.Create(value));

       
        builder.Property(x => x.TenantId).HasColumnName("TenantId")
        .HasMaxLength(10)
        .HasConversion(id => id.Value,
        value => TenantId.Create(value));

        builder.OwnsOne(x=>x.BookRate,br=>{
            br.Property(s=>s.Amount).HasColumnName("BookRate")
            .HasColumnType("decimal(18,2)");
            br.Property(s=>s.Currency).HasMaxLength(3).HasColumnName("Currency");
        });

        builder.Property(s=>s.Discount).HasColumnType("decimal(18,2)");

    }
}