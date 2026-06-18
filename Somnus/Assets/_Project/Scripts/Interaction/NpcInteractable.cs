using UnityEngine;

public class NpcInteractable : InteractableObject
{
    [SerializeField] private Dialog dialog;
    [SerializeField] private bool disableAfterUse;

    private void OnEnable()
    {
        if (disableAfterUse)
            DialogChannel.DialogFinishEvent += OnDialogFinish;
    }

    private void OnDisable()
    {
        DialogChannel.DialogFinishEvent -= OnDialogFinish;
    }

    public override void DoInteraction()
    {
        dialog.Start();
    }

    private void OnDialogFinish(Dialog finished)
    {
        if (finished != dialog) return;

        InteractChannel.Exit();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder -= 1;
    }
}
