using KS.Domain.Entities.UserManagement;
using KS.Domain.UserManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KS.Infrastructure.Persistence.EntityConfigurations.UserManagement
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public  void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(u=>u.Value,
            value=>UserId.Create(value.ToString()))
            .HasMaxLength(60)
            .HasColumnName("UserId");

            builder.Property(u => u.Password).HasMaxLength(500);
            builder.Property(u => u.Username).HasMaxLength(100);
            
            builder.Property(u => u.Fullname).HasMaxLength(100);
            builder.Property(u => u.Department).HasMaxLength(30);
            
            builder.ToTable("User");

            builder.Property(u => u.TenantId)
            .HasMaxLength(60)
            .HasColumnName("TenantId")
            .IsRequired(false);


        }
        private static void SeedUserData(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                User.Create(Guid.NewGuid().ToString(),"admin","Administrator",User.HashPassword("@dmin123"),
                "dev@korrhsolutions.com",null,true,null)
            );
        }
    }
}
