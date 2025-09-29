
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBook.Infrastructure.ViewModel;
using WeebBook.Domain.Constants;

namespace WebBook.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionsController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Permission(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var claims = _roleManager.GetClaimsAsync(role).Result.Select(x => x.Value).ToList();
            var allPermissions = Permissions.PermissionsList()
                .Select(x => new RoleClaimsViewModel
                { Value = x }).ToList();
            foreach (var permission in allPermissions)
            {
                if (claims.Any(x => x == permission.Value))
                    permission.Selected = true;
            }
            return View(new PermissionViewModel
            {
                RoleId = roleId,
                RoleName = role.Name,
                Claims = allPermissions
            });
        }
    }
}
