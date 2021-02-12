using System;

namespace Mathster.Resources.Helpers.Notifications
{
    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}