using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Appointment.Infrastructure.Authentication.Policies
{
    public sealed class OperatorRequirement : AuthorizationHandler<OperatorRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperatorRequirement requirement)
        {
            // todo: Complete this part after finishing all business logics
            /*if (Roles.OperatorRoles.Any(
                operatorRole => context.User.Claims.Any(claim => claim.Value.Contains(operatorRole))))
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