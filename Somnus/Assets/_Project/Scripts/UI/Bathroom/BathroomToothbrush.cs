using UnityEngine;

public class BathroomToothbrush : MonoBehaviour
{
    [SerializeField] private float requiredDistance = 20f;
    [SerializeField] private string nextScene = "HomeMorning";

    public float Progress => Mathf.Clamp01(accumulated / requiredDistance);

    private Rigidbody2D rb;
    private Vector2 prevMouseScreen;
    private Vector2 virtualPos;
    private bool insideZone;
    private float accumulated;
    private bool completed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }

    private void Start()
    {
        virtualPos = rb.position;
        prevMouseScreen = Input.mousePosition;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }

    private void FixedUpdate()
    {
        if (completed || Camera.main == null) return;

        Vector2 screenDelta = (Vector2)Input.mousePosition - prevMouseScreen;
        prevMouseScreen = Input.mousePosition;

        float worldPerPixel = Camera.main.orthographicSize * 2f / Screen.height;
        Vector2 delta = screenDelta * worldPerPixel;
        virtualPos += delta;
        rb.MovePosition(virtualPos);

        if (insideZone)
        {
            accumulated += delta.magnitude;
            if (accumulated >= requiredDistance)
            {
                completed = true;
                Cursor.visible = true;
                UITransitionChannel.TurnOn(nextScene);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TeethZone")) insideZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TeethZone")) insideZone = false;
    }
}
