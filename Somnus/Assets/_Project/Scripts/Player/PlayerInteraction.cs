using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private InteractableObject current;

    private void OnEnable()
    {
        DialogChannel.DialogStartEvent += OnDialogStart;
        DialogChannel.DialogFinishEvent += OnDialogFinish;
    }

    private void OnDisable()
    {
        DialogChannel.DialogStartEvent -= OnDialogStart;
        DialogChannel.DialogFinishEvent -= OnDialogFinish;
    }

    private bool inDialog = false;

    private void Update()
    {
        if (!inDialog && current != null && Input.GetKeyDown(KeyCode.E))
            current.DoInteraction();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InteractableObject obj))
        {
            current = obj;
            InteractChannel.Enter(obj);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out InteractableObject obj) && obj == current)
        {
            current = null;
            InteractChannel.Exit();
        }
    }

    private void OnDialogStart(Dialog _) => inDialog = true;
    private void OnDialogFinish(Dialog _) => inDialog = false;
}
