﻿using System;

namespace Mathster.Resources.Helpers.Notifications
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;
        void SendNotification(string title, string message, DateTime? notifyTime = null);
        void StartService(string title, string message, DateTime? notifyTime = null);
        void ReceiveNotification(string title, string message);
    }
}