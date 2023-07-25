using System.Threading;
using System.Threading.Tasks;

namespace Appointment.Application.Common.Interfaces.Infrastructure
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
    }
}