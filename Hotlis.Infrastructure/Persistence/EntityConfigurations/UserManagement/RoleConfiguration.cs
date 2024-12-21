
using KS.Domain.Entities.UserManagement;
using KS.Domain.UserManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KS.Infrastructure.Persistence.EntityConfigurations.UserManagement;

    public class RoleConfiguration:IEntityTypeConfiguration<Role>
    {     public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(ur => ur.Id);
            builder.Property(ur => ur.Id)
            .ValueGeneratedNever()
            .HasColumnName("RoleId")
            .HasConversion(r=>r.Value,
            value=>RoleId.Create(value))
            .ValueGeneratedOnAdd();

            builder.Property(ur => ur.RoleName).HasMaxLength(50);

            builder.HasMany(e => e.Permissions)
                .WithMany(e => e.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "PermissionRole",
                    j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId")
                );

            builder.HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleUser",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId")
                );

           //SeedRoleData(builder);
            
           
        }
        //seed with initial #pragma warning disable format
        
        private static void SeedRoleData(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                Role.Create("Administrator")
            );
        }
}

