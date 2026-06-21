using UnityEngine;

public class KeyGate : InteractableObject
{
    [SerializeField] private string requiredKeyId;
    [SerializeField] private Collider2D doorCollider;

    public override void DoInteraction()
    {
        if (!Inventory.HasKey(requiredKeyId)) return;

        Inventory.UseKey(requiredKeyId);
        doorCollider.enabled = true;
        NotificationChannel.Show("Дверь открыта!");
        gameObject.SetActive(false);
    }
}
