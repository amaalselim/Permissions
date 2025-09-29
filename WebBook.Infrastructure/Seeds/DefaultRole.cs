using Microsoft.AspNetCore.Identity;
using WeebBook.Domain.Entities;

namespace WebBook.Infrastructure.Seeds
{
    public static class DefaultRole
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Helper.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Helper.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Helper.Roles.Basic.ToString()));
        }
    }
}
