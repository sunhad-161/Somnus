using System;
using UnityEngine;

public static class TeleportChannel
{
    static public Action<Vector3> TeleportEvent;

    static public void Teleport(Vector3 position)
    {
        TeleportEvent?.Invoke(position);
    }
}
