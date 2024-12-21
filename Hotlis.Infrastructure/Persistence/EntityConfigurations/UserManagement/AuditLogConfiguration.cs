using KS.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KS.Infrastructure.Persistence.EntityConfigurations.UserManagement
{
    public class AuditLogConfiguration:IEntityTypeConfiguration<AuditLog>
    {
        public  void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserId)
            .ValueGeneratedOnAdd();
            builder.Property(u => u.UserId).HasMaxLength(60);
            builder.Property(u=>u.UserName).HasMaxLength(100);
            builder.ToTable("AuditLog");

           
        }
    }
}
