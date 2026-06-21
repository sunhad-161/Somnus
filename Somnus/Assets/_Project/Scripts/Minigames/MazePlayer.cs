using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    [SerializeField] private int maxMisses = 3;
    [SerializeField] private string timingScene = "TimingGame";
    [SerializeField] private Vector3 returnPosition;

    private Rigidbody2D rb;
    private Vector2 startPosition;
    private bool needsReset;
    private bool finished;
    private Vector2 virtualPos;
    private Vector2 prevMouseScreen;
    private int missCount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
        Cursor.visible = false;
    }

    private void Start()
    {
        virtualPos = startPosition;
        prevMouseScreen = Input.mousePosition;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }

    private void FixedUpdate()
    {
        if (finished || Camera.main == null) return;

        Vector2 screenDelta = (Vector2)Input.mousePosition - prevMouseScreen;
        prevMouseScreen = Input.mousePosition;

        if (needsReset)
        {
            virtualPos = startPosition;
            needsReset = false;
            rb.MovePosition(virtualPos);
            return;
        }

        float worldPerPixel = Camera.main.orthographicSize * 2f / Screen.height;
        virtualPos += screenDelta * worldPerPixel;
        rb.MovePosition(virtualPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (finished || !other.CompareTag("MazeWall")) return;

        missCount++;
        if (missCount >= maxMisses)
        {
            finished = true;
            SceneMemory.ReturnPosition = returnPosition;
            UITransitionChannel.TurnOn(timingScene);
        }
        else
        {
            needsReset = true;
        }
    }
}
