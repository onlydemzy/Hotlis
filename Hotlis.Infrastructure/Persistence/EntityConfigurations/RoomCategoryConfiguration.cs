using Hotlis.Domain.Entities.ValueObjects;
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

        builder.Property(g => g.Description).HasColumnName("Description")
        .HasMaxLength(100);

        builder.Property(g => g.TenantId).HasColumnName("TenantId")
        .HasConversion(id=>id.Value,value=>TenantId.Create(value))
        .HasMaxLength(10);

        ConfigureRoomRentIds(builder);

    }

    private void ConfigureRoomRentIds(EntityTypeBuilder<RoomCategory> builder)
    {
        builder.OwnsMany(a=>a.RoomRateIds,rr=>{
            rr.ToTable("RoomRateIds");
            rr.WithOwner().HasForeignKey("RoomCategoryId");
            rr.HasKey("Id");
            rr.Property(d=>d.Value)
            .HasColumnName("RoomRateId")
            .ValueGeneratedNever();
        });

        //Explicit configure to populate the underlying lists(i.e _dinnerIds)
        builder.Metadata.FindNavigation(nameof(RoomCategory.RoomRateIds))!
        .SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}