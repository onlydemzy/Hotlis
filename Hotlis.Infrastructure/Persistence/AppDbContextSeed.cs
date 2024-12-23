using Hotlis.Domain.Entities;
using KS.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace Hotlis.Infrastructure.Persistence;
public static class AppDbContextSeed
{
    public static async Task SeedDataSync(AppDbContext context)
    {
        await SeedRoleAsync(context);
        await SeedUserAsync(context);
        await SeedTenantAsync(context);
    }
    private static async Task SeedRoleAsync(AppDbContext context)
    {
        if(!await context.Role.AnyAsync())
        {
            context.Role.Add(Role.Create("Administrator"));
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedUserAsync(AppDbContext context)
    {
        if(!await context.Role.AnyAsync())
        {
            context.User.Add(User.Create(Guid.NewGuid().ToString(),"supadmin","Super Administrator",User.HashPassword("@dmin123"),
                "dev@korrhsolutions.com",null,true,null));
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedTenantAsync(AppDbContext context)
    {
        if (!await context.Role.AnyAsync())
        {
            context.Tenant.Add(Tenant.Create(Guid.NewGuid().ToString(),"MN001","ks@korrhsolutions.com",
            "+2348065592345",null,"Calabar Itu High way","Uyo","Akwa Ibom",
            "Nigeria","Active"));
            await context.SaveChangesAsync();
        }
    }
}