using UnityEngine;
using UnityEngine.UIElements;

public class UIDialogScript : MonoBehaviour
{
    VisualElement Root;
    UIDialogWindow DialogWindow;

    private void Start()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        Root.Clear();

        Root.AddToClassList("root");

        DialogWindow = new UIDialogWindow();
        Root.Add(DialogWindow);

        Root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        DialogChannel.DialogStartEvent += DisplayOn;
        DialogChannel.DialogFinishEvent += DisplayOff;
        DialogChannel.DialogUnFinishEvent += DisplayOn;
    }

    private void OnDisable()
    {
        DialogChannel.DialogStartEvent -= DisplayOn;
        DialogChannel.DialogFinishEvent -= DisplayOff;
        DialogChannel.DialogUnFinishEvent -= DisplayOn;
    }

    private void DisplayOn(Dialog dialog)
    {
        DialogWindow.Clear();
        DialogWindow.Update(dialog);
            
        Root.style.display = DisplayStyle.Flex;
    }

    private void DisplayOff(Dialog dialog)
    {
        Root.style.display = DisplayStyle.None;
    }
}