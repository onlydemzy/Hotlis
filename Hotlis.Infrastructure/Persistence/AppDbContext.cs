
using Hotlis.Domain.Entities;
using Hotlis.Infrastructure.Persistence.EntityConfigurations.UserManagement;
using KS.Domain.Common.Models;
using KS.Domain.Entities.UserManagement;
using KS.Infrastructure.Persistence.EntityConfigurations.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace Hotlis.Infrastructure.Persistence;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    
    //private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor=pDomainEvents;
    //private readonly AuditLogInterceptor _auditLogInterceptor=auditLogInterceptor;
    /*public DbSet<Booking> Booking{get;set;}=null!;
    public DbSet<Payment> Payment{get;set;}=null!;
    public DbSet<Bill> Bill{get;set;}=null!;
    public DbSet<Guest> Guest{get;set; }=null!;
    public DbSet<Room> Room{get;set;}=null!;
    public DbSet<RoomCategory> RoomCategory{get;set; }=null!;
    public DbSet<RoomRates> RoomRates{get;set; }=null!;
    */
    //===============================UserManagement=======================================
    public DbSet<User>  User{get;set; }=null!;
    public DbSet<Role>  Role{get;set; }=null!;
    public DbSet<Permission> Permission{get;set; }=null!;
    public DbSet<AuditLog> AuditLog{get;set; }=null!;
    public DbSet<Tenant> Tenant{get;set; }=null!;
    public DbSet<Menu> Menu{get;set; }=null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new RoomCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new RoomRatesConfiguration());
       
        
        modelBuilder.ApplyConfiguration(new BookingConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new BillConfiguration());
        modelBuilder.ApplyConfiguration(new GuestConfiguration());*/
        //===============================UserManagement=======================================
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
        modelBuilder.ApplyConfiguration(new TenantConfiguration());
        modelBuilder.ApplyConfiguration(new MenuConfiguration());
        
        

        modelBuilder
        .Ignore<List<IDomainEvent>>()
        .ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }


}