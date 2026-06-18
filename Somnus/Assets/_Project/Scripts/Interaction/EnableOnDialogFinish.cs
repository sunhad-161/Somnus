using UnityEngine;

public class EnableOnDialogFinish : MonoBehaviour
{
    [SerializeField] private Dialog dialog;

    private void Awake()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnEnable()
    {
        DialogChannel.DialogFinishEvent += OnDialogFinish;
    }

    private void OnDisable()
    {
        DialogChannel.DialogFinishEvent -= OnDialogFinish;
    }

    private void OnDialogFinish(Dialog finished)
    {
        if (finished != dialog) return;

        GetComponent<Collider2D>().enabled = true;
        enabled = false;
    }
}
