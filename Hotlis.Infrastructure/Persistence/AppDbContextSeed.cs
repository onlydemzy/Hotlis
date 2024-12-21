using KS.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace Hotlis.Infrastructure.Persistence;
public static class AppDbContextSeed
{
    public static async Task SeedRoleAsync(AppDbContext context)
    {
        if(!await context.Role.AnyAsync())
        {
            context.Role.Add(Role.Create("Administrator"));
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedUserAsync(AppDbContext context)
    {
        if(!await context.Role.AnyAsync())
        {
            context.User.Add(User.Create(Guid.NewGuid().ToString(),"admin","Administrator",User.HashPassword("@dmin123"),
                "dev@korrhsolutions.com",null,true,null));
            await context.SaveChangesAsync();
        }
    }
}