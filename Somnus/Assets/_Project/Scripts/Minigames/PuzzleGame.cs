using UnityEngine;

public class PuzzleGame : MonoBehaviour
{
    [SerializeField] private PuzzlePiece[] pieces;
    [SerializeField] private float snapDistance = 0.8f;
    [SerializeField] private string nextScene;

    private Camera cam;
    private PuzzlePiece dragging;
    private Vector3 dragOffset;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector2 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && dragging == null)
        {
            // Pick topmost non-snapped piece under cursor
            var hits = Physics2D.OverlapPointAll(mouseWorld);
            PuzzlePiece best = null;
            int bestOrder = int.MinValue;
            foreach (var h in hits)
            {
                var p = h.GetComponent<PuzzlePiece>();
                if (p == null || p.IsSnapped) continue;
                var psr = h.GetComponent<SpriteRenderer>();
                int order = psr != null ? psr.sortingOrder : 0;
                if (order > bestOrder) { best = p; bestOrder = order; }
            }

            if (best != null)
            {
                dragging = best;
                dragOffset = best.transform.position - (Vector3)mouseWorld;
                dragOffset.z = 0f;
                best.SetDragging(true);
            }
        }

        if (dragging != null)
        {
            dragging.transform.position = (Vector3)mouseWorld + dragOffset;

            if (Input.GetMouseButtonUp(0))
            {
                float dist = Vector3.Distance(dragging.transform.position, dragging.targetPosition);
                if (dist <= snapDistance)
                {
                    dragging.Snap();
                    CheckCompletion();
                }
                else
                {
                    dragging.SetDragging(false);
                }
                dragging = null;
            }
        }
    }

    private void CheckCompletion()
    {
        foreach (var p in pieces)
            if (!p.IsSnapped) return;

        if (!string.IsNullOrEmpty(nextScene))
            UITransitionChannel.TurnOn(nextScene);
    }
}
