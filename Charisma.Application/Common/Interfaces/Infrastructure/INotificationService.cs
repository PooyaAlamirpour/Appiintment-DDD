using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Charisma.Application.Common.Interfaces.Infrastructure
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
    }
}