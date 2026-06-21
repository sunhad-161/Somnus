using UnityEngine;

public class MazeExit : MonoBehaviour
{
    [SerializeField] private string keyToGrant;
    [SerializeField] private string returnScene = "Dream";
    [SerializeField] private Vector3 returnPosition;

    private bool completed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (completed) return;
        if (!other.CompareTag("MazePlayer")) return;

        completed = true;
        SceneMemory.ReturnPosition = returnPosition;
        Inventory.AddKey(keyToGrant);
        UITransitionChannel.TurnOn(returnScene);
    }
}
