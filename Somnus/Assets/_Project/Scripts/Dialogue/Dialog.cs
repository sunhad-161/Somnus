using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObject/Dialog")]
public class Dialog : ScriptableObject
{
    public string DialogName;
    public string NPCText;
    public Dialog PreviousDialog;
    public List<DialogChoice> Choices;

    public void Start() { DialogChannel.StartDialog(this); }
    public void Finish()
    {
        DialogChannel.FinishDialog(this);
    }

    [ContextMenu("Update")]
    public void Update()
    {
        foreach (DialogChoice choice in Choices)
        {
            if (choice.NextDialog == null) continue;

            choice.NextDialog.PreviousDialog = this;
            choice.NextDialog.Update();
        }
    }
}
