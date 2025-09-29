using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebBook.Infrastructure.ViewModel;
using WeebBook.Domain.Constants;
using static WeebBook.Domain.Entities.Helper;

namespace WebBook.Infrastructure.Seeds
{
    public static class DefaultUser
    {
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var DefaultUser = new ApplicationUser
            {
                UserName = UserName,
                Email = Email,
                Name = Name,
                ImageUser = "photo.jpg",
                ActiveUser = true,
                EmailConfirmed = true
            };
            var user = userManager.FindByEmailAsync(DefaultUser.Email);
            if (user.Result == null)
            {
                await userManager.CreateAsync(DefaultUser, Password);
                await userManager.AddToRolesAsync(DefaultUser, new List<string>{
                    Roles.SuperAdmin.ToString(),
                });
            }
        }

        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var DefaultUser = new ApplicationUser
            {
                UserName = UserNameBasic,
                Email = EmailBasic,
                Name = NameBasic,
                ImageUser = "photo.jpg",
                ActiveUser = true,
                EmailConfirmed = true
            };
            var user = userManager.FindByEmailAsync(DefaultUser.Email);
            if (user.Result == null)
            {
                await userManager.CreateAsync(DefaultUser, PasswordBasic);
                await userManager.AddToRolesAsync(DefaultUser, new List<string>{
                    Roles.Basic.ToString(),
                });
            }
            await roleManager.SeedClaimsAsync();

        }
        //Reflection Method
        public static async Task SeedClaimsAsync(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            var modules = Enum.GetValues(typeof(PermissionModuleName));
            foreach (var module in modules)
                await roleManager.AddPermissionClaims(adminRole, module.ToString());
        }
        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == Permission && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim(Permission, permission));
                }
            }
        }
    }
}
