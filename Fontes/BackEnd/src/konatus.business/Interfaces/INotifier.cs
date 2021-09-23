using System.Collections.Generic;

namespace konatus.business.Notifications
{
    public interface INotifier
    {
        bool HasNotification();

        List<Notification> GetNotifications();

        void Handle(Notification notification);
    }
}