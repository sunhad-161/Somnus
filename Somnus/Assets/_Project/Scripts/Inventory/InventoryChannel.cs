using System;

public static class InventoryChannel
{
    public static Action<string> KeyAddedEvent;
    public static Action<string> KeyUsedEvent;

    public static void OnKeyAdded(string id) => KeyAddedEvent?.Invoke(id);
    public static void OnKeyUsed(string id) => KeyUsedEvent?.Invoke(id);
}
