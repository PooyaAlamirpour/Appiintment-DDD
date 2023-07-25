using System.Linq;
using System.Threading.Tasks;
using Charisma.Infrastructure.Authentication.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Charisma.Infrastructure.Authentication.Policies
{
    public sealed class UserRequirement : AuthorizationHandler<UserRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            // todo: Complete this part after finishing all business logics
            /*if (Roles.UserRoles.Any(userRole => context.User.Claims.Any(claim => claim.Value.Contains(userRole))))
            {
                context.Succeed(requirement);
                context.Requirements.ToList().ForEach(context.Succeed);
            }*/

            context.Succeed(requirement);
            context.Requirements.ToList().ForEach(context.Succeed);
            
            return Task.CompletedTask;
        }
    }
}