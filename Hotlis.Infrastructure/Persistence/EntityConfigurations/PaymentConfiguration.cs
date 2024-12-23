using Hotlis.Domain.BillAggragate;
using Hotlis.Domain.BillAggragate.ValueObjects;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.PaymentAggragate;
using Hotlis.Domain.PaymentAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;

public class PaymentConfiguration:IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment");
        builder.HasKey(x => x.Id);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("RoomId")
        .ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(id => id.Value,
        value => PaymentId.Create(value));

        builder.Property(x => x.SubmittedBy).HasMaxLength(50);
        builder.Property(x=>x.PayMethod).HasMaxLength(50);
        builder.Property(x=>x.Description).HasMaxLength(100);

        builder.Property(g => g.TenantId).HasColumnName("TenantId")
        .HasConversion(id => id.Value, value => TenantId.Create(value))
        .HasMaxLength(10);

        builder.OwnsOne(s=>s.Amount,a=>{
            a.Property(s=>s.Currency).HasMaxLength(3).IsRequired()
            .HasColumnName("Currency");
            a.Property(s=>s.Amount).HasColumnType("decimal(18,2)")
            .HasColumnName("Amount");
        });

        
        builder.Property(s=>s.BillId)
        .HasConversion(id=>id.Value,
        value=>BillId.Create(value))
        .HasColumnName("BillId");

        builder.ToTable("Payment");
    }
}