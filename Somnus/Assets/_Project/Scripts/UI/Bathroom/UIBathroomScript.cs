using UnityEngine;
using UnityEngine.UIElements;

public class UIBathroomScript : MonoBehaviour
{
    [SerializeField] private StyleSheet styleSheet;
    [SerializeField] private BathroomToothbrush toothbrush;

    private VisualElement progressFill;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.styleSheets.Add(styleSheet);

        var progressBar = new VisualElement();
        progressBar.AddToClassList("progress-bar");
        progressBar.style.left = Length.Percent(50);
        progressBar.style.bottom = 60;
        progressBar.style.translate = new StyleTranslate(new Translate(Length.Percent(-50), 0));

        progressFill = new VisualElement();
        progressFill.AddToClassList("progress-fill");
        progressBar.Add(progressFill);
        root.Add(progressBar);
    }

    private void Update()
    {
        if (toothbrush != null)
            progressFill.style.width = Length.Percent(toothbrush.Progress * 100f);
    }
}
