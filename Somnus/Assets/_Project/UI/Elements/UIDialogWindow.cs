using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIDialogWindow : VisualElement
{
    public UIDialogWindow()
    { 
        focusable = true;
        AddToClassList("dialogWindow"); 
    }

    public void Update(Dialog dialog)
    {
        Add(new UIDialogText(dialog));

        foreach (DialogChoice dialogChoice in dialog.Choices)
            Add(new UIDialogChoice(dialog, dialogChoice));

        //Add(new UIDialogChoice(dialog));
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIDialogWindow, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}