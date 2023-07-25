using System.Linq;
using System.Threading.Tasks;
using Charisma.Infrastructure.Authentication.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Charisma.Infrastructure.Authentication.Policies
{
    public sealed class ExpertRequirement : AuthorizationHandler<ExpertRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExpertRequirement requirement)
        {
            // todo: Complete this part after finishing all business logics
            /*if (Roles.ExpertRoles.Any(expertRole => context.User.Claims.Any(claim => claim.Value.Contains(expertRole))))
            {
                context.Succeed(requirement);
                context.Requirements.ToList().ForEach(context.Succeed);
            }
            */

            context.Succeed(requirement);
            context.Requirements.ToList().ForEach(context.Succeed);
            
            return Task.CompletedTask;
        }
    }
}