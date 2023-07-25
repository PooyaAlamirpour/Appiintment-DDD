using System;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Common.Interfaces.Infrastructure;

namespace Appointment.Infrastructure.Notifications.Sms
{
    public class SmsService : INotificationService
    {
        public async Task SendNotificationAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"{body}");
        }
    }
}