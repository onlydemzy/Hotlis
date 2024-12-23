using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.GuestAggragate;
using Hotlis.Domain.GuestAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;

public class GuestConfiguration:IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.HasKey(g=>g.Id);
        builder.Property(g => g.Id).HasColumnName("GuestId")
        .ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(id=>id.Value,
        value=>GuestId.Create(value));

        builder.Property(x => x.TenantId).HasColumnName("TenantId")
        .HasMaxLength(10)
        .HasConversion(id => id.Value,
        value => TenantId.Create(value));

        builder.Property(g => g.Lastname).HasMaxLength(50);
        builder.Property(g => g.Othernames).HasMaxLength(100);
        builder.Property(g=>g.Address).HasMaxLength(100).IsRequired(false);
        builder.Property(g=>g.Address).HasMaxLength(100).IsRequired(false);
        builder.Property(g=>g.City).HasMaxLength(50).IsRequired(false);
        builder.Property(g=>g.Country).HasMaxLength(70).IsRequired(false);
        builder.Property(g => g.Email).HasMaxLength(80).IsRequired(false);
        builder.Property(g=>g.Gender).HasMaxLength(20).IsRequired(false);
        builder.Property(g=>g.IdNumber).HasMaxLength(20).IsRequired(false);
        builder.Property(g=>g.IdPath).HasMaxLength(200).IsRequired(false);
        builder.Property(g=>g.IdType).HasMaxLength(60).IsRequired(false);
        builder.Property(g=>g.Phone).HasMaxLength(20);
        builder.Property(g=>g.PhotoPath).HasMaxLength(200).IsRequired(false);
        builder.Property(g=>g.State).HasMaxLength(50).IsRequired(false);
        builder.Property(g=>g.Title).HasMaxLength(20).IsRequired(false);
        builder.ToTable("Guest");

    }
}
