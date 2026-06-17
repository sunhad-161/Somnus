using UnityEngine;
using UnityEngine.UIElements;

public class UIBathroomScript : MonoBehaviour
{
    [SerializeField] private StyleSheet styleSheet;

    private VisualElement cursor;
    private VisualElement progressFill;
    private VisualElement root;
    private float accumulated;
    private bool completed;

    private const float RequiredDistance = 10000f;

    private void Awake()
    {
        UnityEngine.Cursor.visible = false;

        root = GetComponent<UIDocument>().rootVisualElement;
        root.styleSheets.Add(styleSheet);

        cursor = new VisualElement();
        cursor.AddToClassList("fake-cursor");
        cursor.pickingMode = PickingMode.Ignore;
        root.Add(cursor);

        var teethZone = new VisualElement();
        teethZone.AddToClassList("teeth-zone");
        teethZone.style.left = Length.Percent(50);
        teethZone.style.top = Length.Percent(45);
        teethZone.style.translate = new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50)));
        root.Add(teethZone);

        var progressBar = new VisualElement();
        progressBar.AddToClassList("progress-bar");
        progressBar.style.left = Length.Percent(50);
        progressBar.style.bottom = 60;
        progressBar.style.translate = new StyleTranslate(new Translate(Length.Percent(-50), 0));

        progressFill = new VisualElement();
        progressFill.AddToClassList("progress-fill");
        progressBar.Add(progressFill);
        root.Add(progressBar);

        teethZone.RegisterCallback<PointerMoveEvent>(OnTeethMove);
    }

    private void Update()
    {
        if (root?.panel == null) return;

        Vector2 panelPos = RuntimePanelUtils.ScreenToPanel(
            root.panel,
            new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)
        );
        cursor.style.left = panelPos.x - 15;
        cursor.style.top = panelPos.y - 15;
    }

    private void OnDestroy()
    {
        UnityEngine.Cursor.visible = true;
    }

    private void OnTeethMove(PointerMoveEvent e)
    {
        if (completed) return;

        accumulated += e.deltaPosition.magnitude;
        float progress = Mathf.Clamp01(accumulated / RequiredDistance);
        progressFill.style.width = Length.Percent(progress * 100);

        if (progress >= 1f)
        {
            completed = true;
            UnityEngine.Cursor.visible = true;
            UITransitionChannel.TurnOn("Home");
        }
    }
}
