namespace Appointment.Application.Common.Interfaces.Infrastructure
{
    public interface INotificationFactory
    {
        INotificationService Make(NotificationTypes notificationType);
    }

    public enum NotificationTypes
    {
        EMAIL,
        SMS
    }
}