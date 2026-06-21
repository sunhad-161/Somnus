using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] public Vector3 targetPosition;
    [SerializeField] public float targetRotation;

    public bool IsSnapped { get; private set; }

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetDragging(bool isDragging)
    {
        sr.sortingOrder = isDragging ? 10 : 2;
    }

    public void Snap()
    {
        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
        IsSnapped = true;
        sr.sortingOrder = 2;
    }
}
