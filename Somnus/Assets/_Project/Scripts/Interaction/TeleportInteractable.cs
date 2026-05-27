using UnityEngine;

public class TeleportInteractable : InteractableObject
{
    [SerializeField] private Transform destination;

    public override void DoInteraction()
    {
        UITransitionChannel.Blink(() => TeleportChannel.Teleport(destination.position));
    }
}
