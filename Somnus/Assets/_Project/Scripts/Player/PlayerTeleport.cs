using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        TeleportChannel.TeleportEvent += OnTeleport;
    }

    private void OnDisable()
    {
        TeleportChannel.TeleportEvent -= OnTeleport;
    }

    private void OnTeleport(Vector3 position)
    {
        rb.position = position;
    }
}
