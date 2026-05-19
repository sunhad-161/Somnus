using System;

public static class InteractChannel
{
    public static Action<InteractableObject> InteractEnterEvent;
    public static Action InteractExitEvent;

    public static void Enter(InteractableObject obj) => InteractEnterEvent?.Invoke(obj);
    public static void Exit() => InteractExitEvent?.Invoke();
}
