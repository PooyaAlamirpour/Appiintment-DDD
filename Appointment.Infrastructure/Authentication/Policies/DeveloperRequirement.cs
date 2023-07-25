using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Appointment.Infrastructure.Authentication.Policies
{
    public sealed class DeveloperRequirement : AuthorizationHandler<DeveloperRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            DeveloperRequirement requirement)
        {
            // todo: Complete this part after finishing all business logics
            /*if (context.User.Claims.Any(claim => claim.Value.Contains(Roles.DeveloperRole)))
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