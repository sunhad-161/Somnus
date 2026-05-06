using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIDialogChoice : Button
{
    public UIDialogChoice() { AddToClassList("dialogChoice"); }

    public UIDialogChoice(Dialog dialog, DialogChoice dialogChoice) : this ()
    {
        text = dialogChoice.PlayerText;
        Dialog nextDialog = dialogChoice.NextDialog;

        clicked += () => { dialog.Finish(); nextDialog?.Start(); };
    }

    public UIDialogChoice(Dialog dialog) : this()
    {
        text = "Потом поговорим";

        Dialog previousDialog = dialog;
        while (previousDialog.PreviousDialog != null)
            previousDialog = previousDialog.PreviousDialog;

        clicked += () => { dialog.Finish(); DialogChannel.UnFinishDialog(previousDialog); };
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIDialogChoice, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
