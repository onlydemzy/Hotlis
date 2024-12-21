using Hotlis.Domain.RoomCategoryAggragate;
using Hotlis.Domain.RoomCategoryAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;
public class RoomCategoryConfiguration:IEntityTypeConfiguration<RoomCategory>
{
    public void Configure(EntityTypeBuilder<RoomCategory> builder)
    {
        builder.ToTable("RoomCategory");
        builder.HasKey(x => x.Id);
        builder.Property(g => g.Id).HasColumnName("RoomCategoryId")
        .ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(id=>id.Value,
        value=>RoomCategoryId.Create(value));

        builder.Property(g => g.Description).HasMaxLength(100);
        builder.Property(g => g.Title).HasMaxLength(80);
        

    }
}