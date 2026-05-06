using System;

public static class DialogChannel
{
    static public Action<Dialog> DialogStartEvent;
    static public Action<Dialog> DialogFinishEvent;
    static public Action<Dialog> DialogUnFinishEvent;

    static public void StartDialog(Dialog dialog)
    {
        DialogStartEvent?.Invoke(dialog);
    }

    static public void FinishDialog(Dialog dialog)
    {
        DialogFinishEvent?.Invoke(dialog);
    }

    static public void UnFinishDialog(Dialog dialog)
    {
        DialogUnFinishEvent?.Invoke(dialog);
    }
}
