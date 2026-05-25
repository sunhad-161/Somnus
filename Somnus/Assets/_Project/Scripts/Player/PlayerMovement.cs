using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private Vector3 baseScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseScale = transform.localScale;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        animator.SetBool("IsWalking", moveInput.x != 0);
    }

    void FixedUpdate()
    {
        Vector2 nextPosition = rb.position + moveInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);

        if (moveInput.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x) * Mathf.Abs(baseScale.x), baseScale.y, baseScale.z);
    }
}
