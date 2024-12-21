using Hotlis.Domain.BillAggragate;
using Hotlis.Domain.BillAggragate.ValueObjects;
using Hotlis.Domain.PaymentAggragate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;

public class PaymentConfiguration:IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SubmittedBy).HasMaxLength(50);
        builder.Property(x=>x.PayMethod).HasMaxLength(50);
        builder.Property(x=>x.Description).HasMaxLength(100);
        
        builder.OwnsOne(s=>s.Amount,a=>{
            a.Property(s=>s.Currency).HasMaxLength(3).IsRequired()
            .HasColumnName("Currency");
            a.Property(s=>s.Amount).HasColumnType("decimal(18,2)")
            .HasColumnType("Amount");
        });

        builder.HasOne<Bill>().WithMany()
        .HasForeignKey(s=>s.BillId);

        builder.Property(s=>s.BillId)
        .HasConversion(id=>id.Value,
        value=>BillId.Create(value))
        .HasColumnName("BillId");

        builder.ToTable("Payment");
    }
}