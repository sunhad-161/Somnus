using System;

public static class NotificationChannel
{
    public static Action<string> ShowEvent;
    public static void Show(string message) => ShowEvent?.Invoke(message);
}
