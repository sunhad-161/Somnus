using UnityEngine;

public class KeyPickup : InteractableObject
{
    [SerializeField] private string keyId;

    public override void DoInteraction()
    {
        Inventory.AddKey(keyId);
        gameObject.SetActive(false);
    }
}
