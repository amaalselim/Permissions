
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebBook.Infrastructure.ViewModel;
using WeebBook.Domain.Constants;
using WeebBook.Domain.Entities;

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
            if (string.IsNullOrEmpty(roleId))
                return BadRequest("Role Id is required");

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound($"Role with Id = {roleId} was not found.");
            }
            var claims = (await _roleManager.GetClaimsAsync(role)).Select(x => x.Value).ToList();
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

        public async Task<IActionResult> Roles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                return NotFound($"Role with Id = {model.RoleId} was not found.");
            }
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.Claims.Where(x => x.Selected).ToList();
            foreach (var claim in selectedClaims)
                await _roleManager.AddClaimAsync(role, new Claim(Helper.Permission, claim.Value));

            return RedirectToAction("Roles");
        }

    }
}
