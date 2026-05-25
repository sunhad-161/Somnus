using UnityEngine;
using UnityEngine.UIElements;

public class UIInteractScript : MonoBehaviour
{
    [SerializeField] private StyleSheet styleSheet;

    private UIInteractMark mark;
    private Transform target;
    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.styleSheets.Add(styleSheet);

        mark = new UIInteractMark();
        mark.style.display = DisplayStyle.None;
        root.Add(mark);
    }

    private void OnEnable()
    {
        InteractChannel.InteractEnterEvent += Show;
        InteractChannel.InteractExitEvent += Hide;
    }

    private void OnDisable()
    {
        InteractChannel.InteractEnterEvent -= Show;
        InteractChannel.InteractExitEvent -= Hide;
    }

    private void Update()
    {
        if (target == null || root.panel == null) return;

        Vector2 pos = RuntimePanelUtils.CameraTransformWorldToPanel(
            root.panel,
            target.position + Vector3.up * 2f,
            Camera.main
        );

        mark.style.left = pos.x;
        mark.style.top = pos.y;
    }

    private void Show(InteractableObject obj)
    {
        target = obj.transform;
        mark.style.display = DisplayStyle.Flex;
    }

    private void Hide()
    {
        target = null;
        mark.style.display = DisplayStyle.None;
    }
}
