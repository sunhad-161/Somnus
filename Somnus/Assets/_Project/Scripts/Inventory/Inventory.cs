using System.Collections.Generic;

public static class Inventory
{
    private static readonly HashSet<string> keys = new();

    public static bool HasKey(string id) => keys.Contains(id);

    public static void AddKey(string id)
    {
        keys.Add(id);
        InventoryChannel.OnKeyAdded(id);
    }

    public static void UseKey(string id)
    {
        keys.Remove(id);
        InventoryChannel.OnKeyUsed(id);
    }
}
