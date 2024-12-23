using KS.Domain.Entities.UserManagement;
using KS.Domain.UserManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KS.Infrastructure.Persistence.EntityConfigurations.UserManagement
{
    public class PermissionConfiguration:IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
             .HasConversion(id=>id.Value,
            value=>PermissionId.Create(value))
            .HasMaxLength(60)
            .HasColumnName("PermissionId")
            .HasDefaultValueSql("NEWID()")
            .ValueGeneratedOnAdd();

           
            builder.Property(r => r.Resource).HasMaxLength(100);
            builder.Property(a => a.Module).HasMaxLength(70).IsRequired(false);
            
            
        }
    }
}
