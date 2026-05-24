using UnityEngine;

public class SceneTransitionInteractable : InteractableObject
{
    [SerializeField] private string targetScene;

    public override void DoInteraction()
    {
        UITransitionChannel.TurnOn(targetScene);
    }
}
