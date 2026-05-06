using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        Vector2 nextPosition = rb.position + moveInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);

        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
        }
    }
}
