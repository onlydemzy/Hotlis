using Hotlis.Domain.BillAggragate;
using Hotlis.Domain.BillAggragate.ValueObjects;
using Hotlis.Domain.BookingAggragate;
using Hotlis.Domain.BookingAggragate.ValueObjects;
using Hotlis.Domain.RoomAggragate;
using Hotlis.Domain.RoomAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;
public class BillConfiguration:IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("BillId")
        .ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(id=>id.Value,
        value=>BillId.Create(value));
        builder.Property(x => x.BilledTo).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(150);
        builder.Property(x=>x.Service).HasMaxLength(100);

        builder.OwnsOne(x => x.AmountDue,
            a=>{
                a.Property(x=>x.Currency).HasMaxLength(3).IsRequired();
                a.Property(x=>x.Amount).HasColumnType("decimal(18,2)");
            });

        builder.HasOne<Booking>().WithMany()
            .HasForeignKey(x => x.BookingId);

        builder.HasOne<Room>().WithMany()
            .HasForeignKey(x => x.RoomId);

        builder.Property(a=>a.BookingId)
        .HasColumnName("BookingId")
        .HasMaxLength(60)
        .HasConversion(di=>di!=null?di.Value:null,
        value=>value!=null?(BookingId?)BookingId.Create(value):null)
        .IsRequired(false);

        builder.Property(a=>a.RoomId)
        .HasColumnName("RoomId")
        .HasMaxLength(60)
        .HasConversion(di=>di!=null?di.Value:null,
        value=>value!=null?(RoomId?)RoomId.Create(value):null)
        .IsRequired(false);

        builder.ToTable("Bill");

    }

   
}