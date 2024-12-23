using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.RoomAggragate;
using Hotlis.Domain.RoomAggragate.ValueObjects;
using Hotlis.Domain.RoomCategoryAggragate;
using Hotlis.Domain.RoomCategoryAggragate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotlis.Infrastructure.Persistence.EntityConfigurations;
public class RoomConfiguration:IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("RoomId")
        .ValueGeneratedNever()
        .HasMaxLength(60)
        .HasConversion(id=>id.Value,
        value=>RoomId.Create(value));
        
        builder.Property(a=>a.RoomNumber);
        builder.Property(g => g.TenantId).HasColumnName("TenantId")
        .HasConversion(id => id.Value, value => TenantId.Create(value))
        .HasMaxLength(10);

        builder.Property(s=>s.RoomCategoryId)
        .HasConversion(s=>s.Value,id=>RoomCategoryId.Create(id))
        .HasMaxLength(60)
        .ValueGeneratedNever();
          
    }
}