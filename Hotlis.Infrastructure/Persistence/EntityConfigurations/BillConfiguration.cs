using Hotlis.Domain.BillAggragate;
using Hotlis.Domain.BillAggragate.ValueObjects;
using Hotlis.Domain.BookingAggragate.ValueObjects;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.GuestAggragate.ValueObjects;
using Hotlis.Domain.RoomAggragate.ValueObjects;
using Hotlis.Domain.ServiceConsumedAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;
public class BillConfiguration:IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("Bill");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("BillId")
        .ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(id=>id.Value,
        value=>BillId.Create(value));

        builder.Property(x => x.TenantId).HasColumnName("TenantId")
        .HasMaxLength(10)
        .HasConversion(id => id.Value,
        value => TenantId.Create(value));

        //Configure linear properties
        builder.Property(x => x.BilledTo).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(150);
        builder.Property(x=>x.TenantId)
        .HasConversion(x=>x.Value, value=>TenantId.Create(value))
        .HasMaxLength(10);


        builder.OwnsOne(x => x.AmountDue,
            a=>{
                a.Property(x=>x.Currency).HasMaxLength(3);
                a.Property(x=>x.Amount).HasColumnType("decimal(18,2)");
            });

        builder.Property(b=>b.ConsumedServiceId)
        .HasConversion(a=>a.Value,value=>ConsumedServiceId.Create(value));
        ConfigureOptionalKeys(builder);

        //configure Payments
        builder.OwnsMany(a=>a.PaymentIds,bp=>{
            bp.ToTable("BillPaymentIds");
            bp.WithOwner().HasForeignKey("BillId");
            bp.HasKey("Id");
            bp.Property(i=>i.Value)
            .HasColumnName("PaymentId");
        });

    }

    private static void ConfigureOptionalKeys(EntityTypeBuilder<Bill> builder)
    {
        builder.Property(a=>a.BookingId)
        .HasConversion(id=>id!=null?id.Value:null,
        value=>value!=null?(BookingId?)BookingId.Create(value):null)
        .IsRequired(false);

        builder.Property(a => a.RoomId)
        .HasConversion(id => id != null ? id.Value : null,
        value => value != null ? (RoomId?)RoomId.Create(value) : null)
        .IsRequired(false);

        builder.Property(a => a.GuestId)
        .HasConversion(id => id != null ? id.Value : null,
        value => value != null ? (GuestId?)GuestId.Create(value) : null)
        .IsRequired(false);
    }

   
}