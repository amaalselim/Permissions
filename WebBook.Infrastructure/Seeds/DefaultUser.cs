using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebBook.Infrastructure.ViewModel;
using WeebBook.Domain.Entities;

namespace WebBook.Infrastructure.Seeds
{
    public class DefaultUser
    {
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var DefaultUser = new ApplicationUser
            {
                UserName = Helper.UserName,
                Email = Helper.Email,
                Name = Helper.Name,
                ImageUser = "photo.jpg",
                ActiveUser = true,
                EmailConfirmed = true
            };
            var user = userManager.FindByEmailAsync(DefaultUser.Email);
            if (user.Result == null)
            {
                await userManager.CreateAsync(DefaultUser, Helper.Password);
                await userManager.AddToRoleAsync(DefaultUser.Id, Helper.Roles.SuperAdmin.ToString());
            }
        }

        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var DefaultUser = new ApplicationUser
            {
                UserName = Helper.UserNameBasic,
                Email = Helper.EmailBasic,
                Name = Helper.NameBasic,
                ImageUser = "photo.jpg",
                ActiveUser = true,
                EmailConfirmed = true
            };
            var user = userManager.FindByEmailAsync(DefaultUser.Email);
            if (user.Result == null)
            {
                await userManager.CreateAsync(DefaultUser, Helper.PasswordBasic);
                await userManager.AddToRoleAsync(DefaultUser.Id, Helper.Roles.SuperAdmin.ToString());
            }
        }
    }
}
