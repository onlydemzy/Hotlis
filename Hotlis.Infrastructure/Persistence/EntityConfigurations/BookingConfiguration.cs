using Hotlis.Domain.BookingAggragate;
using Hotlis.Domain.BookingAggragate.ValueObjects;
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

        builder.HasOne<Room>().WithMany()
        .HasForeignKey(r => r.RoomId);

        builder.HasOne<RoomCategoryId>().WithMany()
        .HasForeignKey(r=>r.RoomCategoryId);

        builder.Property(a=>a.RoomCategoryId)
        .HasConversion(s=>s.Value,
        value=>RoomCategoryId.Create(value))
        .HasMaxLength(60)
        .HasColumnName("RoomCategoryId");

        builder.Property(a=>a.RoomId)
        .HasColumnName("RoomId")
        .HasMaxLength(60)
        .HasConversion(di=>di!=null?di.Value:null,
        value=>value!=null?(RoomId?)RoomId.Create(value):null)
        .IsRequired(false);

    }
}