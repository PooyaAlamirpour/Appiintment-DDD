using System;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Infrastructure;
using Microsoft.Extensions.Options;

namespace Charisma.Infrastructure.Notifications.Email
{
    public sealed class EmailService : INotificationService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendNotificationAsync(string to, string subject, string body,
            CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"{_settings.SenderDisplayName} => Subject:{subject} - {body}");
        }
    }
}