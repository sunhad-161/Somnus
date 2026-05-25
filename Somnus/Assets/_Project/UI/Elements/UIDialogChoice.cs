using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIDialogChoice : Button
{
    public UIDialogChoice() { AddToClassList("dialogChoice"); }

    public UIDialogChoice(Dialog dialog, DialogChoice dialogChoice) : this ()
    {
        text = dialogChoice.PlayerText;
        AddToClassList("dialogChoice--" + dialogChoice.Type.ToString().ToLower());
        Dialog nextDialog = dialogChoice.NextDialog;

        clicked += () =>
        {
            if (nextDialog != null)
                nextDialog.Start();
            dialog.Finish();
        };
    }

    public UIDialogChoice(Dialog dialog) : this()
    {
        text = "����� ���������";

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
