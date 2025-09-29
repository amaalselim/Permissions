using Microsoft.AspNetCore.Authorization;
using WeebBook.Domain.Entities;

namespace WebBook.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler()
        {

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null) //No one make login
                return;

            var Permission = context.User.Claims
                .Where(x => x.Type == Helper.Permission && x.Value == requirement.Permission && x.Issuer == "LOCAL AUTHORITY");

            if (Permission.Any())
            {
                context.Succeed(requirement);
                return;
            }


        }
    }
}
