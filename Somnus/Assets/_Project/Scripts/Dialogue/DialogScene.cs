using UnityEngine;

public class DialogScene : MonoBehaviour
{
    [SerializeField] SceneReference NextScene;
    [SerializeField] Dialog Dialog;

    private void OnEnable()
    {
        DialogChannel.DialogFinishEvent += Check;
    }

    private void OnDisable()
    {
        DialogChannel.DialogFinishEvent -= Check;
    }

    void Check(Dialog dialog)
    {
        if (dialog.Equals(Dialog))
            UITransitionChannel.TurnOn(NextScene);
    }
}
