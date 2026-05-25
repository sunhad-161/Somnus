using UnityEngine;

public class SceneTransitionInteractable : InteractableObject
{
    [SerializeField] private SceneReference targetScene;

    public override void DoInteraction()
    {
        UITransitionChannel.TurnOn(targetScene);
    }
}
