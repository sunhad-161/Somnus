using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogScene : MonoBehaviour
{
    [SerializeField] string NextScene;
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
            SceneManager.LoadScene(NextScene);
    }
}
