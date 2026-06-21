using UnityEngine;

public class DreamSpawner : MonoBehaviour
{
    private void Start()
    {
        if (SceneMemory.ReturnPosition == null) return;

        var player = GameObject.FindWithTag("Player");
        if (player == null) return;

        player.GetComponent<Rigidbody2D>().position = SceneMemory.ReturnPosition.Value;
        Physics2D.SyncTransforms();
        SceneMemory.ReturnPosition = null;
    }
}
