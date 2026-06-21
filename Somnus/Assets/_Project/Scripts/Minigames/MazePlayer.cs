using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }

    private void FixedUpdate()
    {
        if (Camera.main == null) return;
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.MovePosition(mouseWorld);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MazeWall"))
            rb.position = startPosition;
    }
}
