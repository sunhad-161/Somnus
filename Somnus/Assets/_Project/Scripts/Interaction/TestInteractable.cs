using UnityEngine;

public class TestInteractable : InteractableObject
{
    public override void DoInteraction()
    {
        Debug.Log("Взаимодействие работает!");
    }
}
