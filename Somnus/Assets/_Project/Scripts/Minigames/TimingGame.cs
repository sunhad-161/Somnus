using UnityEngine;

public class TimingGame : MonoBehaviour
{
    [Header("Размеры")]
    [SerializeField] private float radius = 3f;
    [SerializeField] private float ringThickness = 0.15f;
    [SerializeField] private float markerSize = 0.22f;

    [Header("Цвета")]
    [SerializeField] private Color ringColor = Color.white;
    [SerializeField] private Color sectorColor = new Color(0.25f, 0.85f, 0.35f, 1f);
    [SerializeField] private Color markerColor = Color.red;

    [Header("Фоны (по порядку: старт, промах 1, промах 2)")]
    [SerializeField] private Sprite[] backgroundSprites;
    [SerializeField] private string gameOverScene = "";

    [Header("Игра")]
    [SerializeField] private float markerSpeed = 120f;
    [SerializeField] private float speedIncreasePerHit = 15f;
    [SerializeField] private float sectorAngle = 60f;
    [SerializeField] private float sectorShrinkPerHit = 6f;
    [SerializeField] private float sectorMinAngle = 10f;
    [SerializeField] private int totalRounds = 5;
    [SerializeField] private string nextScene = "";

    private float markerDeg;
    private float currentSpeed;
    private float markerDirection = 1f;
    private float sectorStartDeg;
    private float currentSectorAngle;
    private int roundsDone;
    private int missCount;
    private bool finished;
    private Transform markerPivot;
    private MeshFilter sectorFilter;
    private SpriteRenderer bgRenderer;

    private const int MaxMisses = 3;
    private const int Segments = 64;

    private void Awake()
    {
        bgRenderer = CreateBackground();
        CreateRing();
        sectorFilter = CreateSectorObject();
        markerPivot = CreateMarkerPivot();
    }

    private void Start()
    {
        currentSpeed = markerSpeed;
        currentSectorAngle = sectorAngle;
        ApplyBackground(0);
        PlaceNewSector();
    }

    private void Update()
    {
        if (finished) return;

        markerDeg = (markerDeg + currentSpeed * markerDirection * Time.deltaTime % 360f + 360f) % 360f;
        markerPivot.localRotation = Quaternion.Euler(0f, 0f, -markerDeg);

        if (Input.GetKeyDown(KeyCode.Space))
            CheckHit();
    }

    private void CheckHit()
    {
        float sectorMid = sectorStartDeg + currentSectorAngle * 0.5f;
        float diff = Mathf.Abs(Mathf.DeltaAngle(markerDeg, sectorMid));

        if (diff <= currentSectorAngle * 0.5f)
            OnHit();
        else
            OnMiss();
    }

    private void OnHit()
    {
        roundsDone++;
        currentSpeed += speedIncreasePerHit;
        markerDirection *= -1f;
        currentSectorAngle = Mathf.Max(sectorMinAngle, currentSectorAngle - sectorShrinkPerHit);

        if (roundsDone >= totalRounds)
            EndGame(nextScene);
        else
            PlaceNewSector();
    }

    private void OnMiss()
    {
        missCount++;
        ApplyBackground(missCount);

        if (missCount >= MaxMisses)
            EndGame(gameOverScene);
    }

    private void EndGame(string scene)
    {
        finished = true;
        if (!string.IsNullOrEmpty(scene))
            UITransitionChannel.TurnOn(scene);
    }

    private void ApplyBackground(int index)
    {
        if (bgRenderer == null) return;
        if (backgroundSprites == null || index >= backgroundSprites.Length) return;

        bgRenderer.sprite = backgroundSprites[index];
        FitToScreen(bgRenderer);
    }

    private void FitToScreen(SpriteRenderer sr)
    {
        if (sr.sprite == null || Camera.main == null) return;

        float worldH = Camera.main.orthographicSize * 2f;
        float worldW = worldH * Camera.main.aspect;
        Vector2 spriteSize = sr.sprite.bounds.size;
        sr.transform.localScale = new Vector3(worldW / spriteSize.x, worldH / spriteSize.y, 1f);
    }

    private void PlaceNewSector()
    {
        sectorStartDeg = Random.Range(0f, 360f);
        RebuildSector();
    }

    private void RebuildSector()
    {
        sectorFilter.mesh = BuildArcMesh(radius, ringThickness * 1.6f, sectorStartDeg, currentSectorAngle, Segments);
    }

    // ──── Object creators ────

    private SpriteRenderer CreateBackground()
    {
        var go = new GameObject("Background");
        go.transform.SetParent(transform, false);
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sortingOrder = -1;
        return sr;
    }

    private void CreateRing()
    {
        var go = new GameObject("Ring");
        go.transform.SetParent(transform, false);
        var mf = go.AddComponent<MeshFilter>();
        var mr = go.AddComponent<MeshRenderer>();
        mr.material = MakeMaterial(ringColor);
        mr.sortingOrder = 1;
        mf.mesh = BuildRingMesh(radius, ringThickness, Segments);
    }

    private MeshFilter CreateSectorObject()
    {
        var go = new GameObject("Sector");
        go.transform.SetParent(transform, false);
        var mf = go.AddComponent<MeshFilter>();
        var mr = go.AddComponent<MeshRenderer>();
        mr.material = MakeMaterial(sectorColor);
        mr.sortingOrder = 2;
        return mf;
    }

    private Transform CreateMarkerPivot()
    {
        var pivot = new GameObject("MarkerPivot");
        pivot.transform.SetParent(transform, false);

        var dot = new GameObject("Marker");
        dot.transform.SetParent(pivot.transform, false);
        dot.transform.localPosition = new Vector3(0f, radius, 0f);

        var mf = dot.AddComponent<MeshFilter>();
        var mr = dot.AddComponent<MeshRenderer>();
        mr.material = MakeMaterial(markerColor);
        mr.sortingOrder = 3;
        mf.mesh = BuildCircleMesh(markerSize, 24);

        return pivot.transform;
    }

    // ──── Mesh builders ────
    // Углы: 0° = верх, по часовой стрелке

    private static Mesh BuildRingMesh(float r, float thickness, int seg)
    {
        float inner = r - thickness * 0.5f;
        float outer = r + thickness * 0.5f;

        var verts = new Vector3[seg * 2];
        var tris = new int[seg * 6];

        for (int i = 0; i < seg; i++)
        {
            float a = Mathf.Deg2Rad * (90f - 360f * i / seg);
            float cos = Mathf.Cos(a), sin = Mathf.Sin(a);
            verts[i * 2]     = new Vector3(cos * inner, sin * inner, 0f);
            verts[i * 2 + 1] = new Vector3(cos * outer, sin * outer, 0f);

            int next = (i + 1) % seg;
            int t = i * 6;
            tris[t]     = i * 2;     tris[t + 1] = next * 2;     tris[t + 2] = i * 2 + 1;
            tris[t + 3] = next * 2;  tris[t + 4] = next * 2 + 1; tris[t + 5] = i * 2 + 1;
        }

        return MakeMesh(verts, tris);
    }

    private static Mesh BuildArcMesh(float r, float thickness, float startDeg, float angleDeg, int seg)
    {
        float inner = r - thickness * 0.5f;
        float outer = r + thickness * 0.5f;

        var verts = new Vector3[(seg + 1) * 2];
        var tris  = new int[seg * 6];

        for (int i = 0; i <= seg; i++)
        {
            float a = Mathf.Deg2Rad * (90f - (startDeg + angleDeg * i / seg));
            float cos = Mathf.Cos(a), sin = Mathf.Sin(a);
            verts[i * 2]     = new Vector3(cos * inner, sin * inner, 0f);
            verts[i * 2 + 1] = new Vector3(cos * outer, sin * outer, 0f);
        }

        for (int i = 0; i < seg; i++)
        {
            int t = i * 6;
            tris[t]     = i * 2;        tris[t + 1] = (i + 1) * 2;     tris[t + 2] = i * 2 + 1;
            tris[t + 3] = (i + 1) * 2; tris[t + 4] = (i + 1) * 2 + 1; tris[t + 5] = i * 2 + 1;
        }

        return MakeMesh(verts, tris);
    }

    private static Mesh BuildCircleMesh(float r, int seg)
    {
        var verts = new Vector3[seg + 1];
        var tris  = new int[seg * 3];

        verts[0] = Vector3.zero;
        for (int i = 0; i < seg; i++)
        {
            float a = Mathf.Deg2Rad * (360f * i / seg);
            verts[i + 1] = new Vector3(Mathf.Cos(a) * r, Mathf.Sin(a) * r, 0f);
        }

        for (int i = 0; i < seg; i++)
        {
            tris[i * 3]     = 0;
            tris[i * 3 + 1] = i + 1;
            tris[i * 3 + 2] = (i + 1) % seg + 1;
        }

        return MakeMesh(verts, tris);
    }

    private static Mesh MakeMesh(Vector3[] verts, int[] tris)
    {
        var m = new Mesh { vertices = verts, triangles = tris };
        m.RecalculateNormals();
        return m;
    }

    private static Material MakeMaterial(Color color)
    {
        var mat = new Material(Shader.Find("Sprites/Default")) { color = color };
        return mat;
    }
}
