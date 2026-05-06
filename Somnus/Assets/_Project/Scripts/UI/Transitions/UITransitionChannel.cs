using System;

static public class UITransitionChannel
{
    static public Action<string> UITransitionOnEvent;
    static public Action UITransitionOffEvent;
    static public Action UITransitionBlinkEvent;

    static public void TurnOn(string NextScene)
    {
        UITransitionOnEvent?.Invoke(NextScene);
    }

    static public void TurnOff()
    {
        UITransitionOffEvent?.Invoke();
    }

    static public void Blink()
    {
        UITransitionBlinkEvent?.Invoke();
    }
}
