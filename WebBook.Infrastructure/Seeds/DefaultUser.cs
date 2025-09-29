using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebBook.Infrastructure.ViewModel;
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
                await userManager.AddToRoleAsync(DefaultUser.Id, Roles.SuperAdmin.ToString());
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
                await userManager.AddToRoleAsync(DefaultUser.Id, Roles.Basic.ToString());
            }

            //Code Seeding Claims

        }
        //Reflection Method
        public static async Task SeedClaimsAsync(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
        }
    }
}
