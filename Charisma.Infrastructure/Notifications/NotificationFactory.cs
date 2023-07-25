using System;
using Charisma.Application.Common.Interfaces.Infrastructure;
using Charisma.Infrastructure.Notifications.Email;
using Charisma.Infrastructure.Notifications.Sms;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.Infrastructure.Notifications
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public INotificationService Make(NotificationTypes notificationType)
        {
            return notificationType switch
            {
                NotificationTypes.EMAIL => _serviceProvider.GetRequiredService<EmailService>(),
                NotificationTypes.SMS => _serviceProvider.GetRequiredService<SmsService>(),
                _ => throw new ArgumentOutOfRangeException(nameof(notificationType), notificationType, null)
            };
        }
    }
}