using UnityEngine.UIElements;

public class UIInteractMark : VisualElement
{
    public UIInteractMark()
    {
        AddToClassList("interactMark");
        style.translate = new Translate(Length.Percent(-50), 0);
    }
}
