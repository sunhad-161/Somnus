using UnityEngine;

public class NpcInteractable : InteractableObject
{
    [SerializeField] private Dialog dialog;

    public override void DoInteraction()
    {
        dialog.Start();
    }
}
