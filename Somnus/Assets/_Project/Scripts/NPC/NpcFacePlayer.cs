using UnityEngine;

public class NpcFacePlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
        var playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        float dir = player.position.x - transform.position.x;
        if (dir == 0) return;

        transform.localScale = new Vector3(
            Mathf.Sign(dir) * Mathf.Abs(baseScale.x),
            baseScale.y,
            baseScale.z
        );
    }
}
