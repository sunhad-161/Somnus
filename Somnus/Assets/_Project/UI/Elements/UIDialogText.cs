using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIDialogText : TextElement
{ 
    public UIDialogText() { AddToClassList("dialogText"); }
    public UIDialogText(Dialog dialog) : this() { text = dialog.NPCText; }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIDialogText, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}