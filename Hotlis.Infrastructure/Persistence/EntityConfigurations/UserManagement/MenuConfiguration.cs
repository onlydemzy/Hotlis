using KS.Domain.Entities.UserManagement;
using KS.Domain.UserManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KS.Infrastructure.Persistence.EntityConfigurations.UserManagement
{
    public class MenuConfiguration:IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");
            builder.HasKey(m => m.Id);
            builder.Property(r => r.Id)
            .HasConversion(id=>id.Value,
            value=>MenuId.Create(value))
            .HasMaxLength(60)
            .HasColumnName("MenuId");

            builder.Property(m=>m.PermissionId)
            .HasConversion(id=>id.Value,
            value=>PermissionId.Create(value))
            .HasMaxLength(60)
            .HasDefaultValueSql("NEWID()")
            .ValueGeneratedOnAdd();

            builder.Property(m => m.Name).HasMaxLength(50).IsRequired();
            builder.Property(m => m.Heading).HasMaxLength(70).IsRequired(false);
            builder.Property(m => m.Resource).HasMaxLength(100).IsRequired(false);
            builder.Property(m => m.Collapse).HasMaxLength(100).IsRequired(false); ;
            builder.Property(m => m.Heading).HasMaxLength(100).IsRequired(false); ;
            builder.Property(m => m.Icon).HasMaxLength(100).IsRequired(false);

            builder.HasMany(m => m.ChildrenMenus)
            .WithOne()
            .HasForeignKey("ParentMenuId");
            
            //Explicitely imform EF of the navigation property
            builder.Metadata.FindNavigation(nameof(Menu.ChildrenMenus))!
        .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(m => m.AlwaysEnable).IsRequired(true);
            
        }
    }
}
