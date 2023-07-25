using System;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Infrastructure;

namespace Charisma.Infrastructure.Notifications.Sms
{
    public class SmsService : INotificationService
    {
        public async Task SendNotificationAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"{body}");
        }
    }
}