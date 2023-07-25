using System;
using Appointment.Application.Common.Interfaces.Infrastructure;
using Appointment.Infrastructure.Notifications.Email;
using Appointment.Infrastructure.Notifications.Sms;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Infrastructure.Notifications
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