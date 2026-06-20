using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIOfficeScript : MonoBehaviour
{
    [SerializeField] private StyleSheet styleSheet;
    [SerializeField] private int documentCount = 5;
    [SerializeField] private string nextScene = "HomeMorning";

    private VisualElement root;
    private Label targetLabel;
    private List<int> stampOrder;
    private int currentStampIndex;

    private VisualElement draggedDoc;
    private Vector2 dragOffset;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.styleSheets.Add(styleSheet);
        Setup();
    }

    private void Setup()
    {
        targetLabel = new Label();
        targetLabel.AddToClassList("target-label");
        root.Add(targetLabel);

        stampOrder = GenerateShuffled(documentCount);
        var positions = GenerateShuffled(documentCount);

        for (int i = 0; i < documentCount; i++)
            root.Add(CreateDocument(positions[i]));

        UpdateTargetLabel();
    }

    private VisualElement CreateDocument(int number)
    {
        var doc = new VisualElement();
        doc.AddToClassList("document");
        doc.userData = number;
        doc.style.left = Random.Range(80, 900);
        doc.style.top = Random.Range(150, 480);

        var numLabel = new Label($"№{number}");
        numLabel.AddToClassList("document-number");
        numLabel.pickingMode = PickingMode.Ignore;
        doc.Add(numLabel);

        var stampMark = new VisualElement();
        stampMark.AddToClassList("stamp-mark");
        stampMark.pickingMode = PickingMode.Ignore;
        stampMark.style.display = DisplayStyle.None;
        doc.Add(stampMark);

        doc.RegisterCallback<PointerDownEvent>(OnPointerDown);
        doc.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        doc.RegisterCallback<PointerUpEvent>(OnPointerUp);

        return doc;
    }

    private void OnPointerDown(PointerDownEvent e)
    {
        var doc = e.currentTarget as VisualElement;

        if (e.button == 0)
        {
            draggedDoc = doc;
            draggedDoc.BringToFront();
            draggedDoc.CapturePointer(e.pointerId);
            dragOffset = new Vector2(e.localPosition.x, e.localPosition.y);
        }
        else if (e.button == 1)
        {
            TryStamp(doc);
        }

        e.StopPropagation();
    }

    private void OnPointerMove(PointerMoveEvent e)
    {
        if (draggedDoc == null || !draggedDoc.HasPointerCapture(e.pointerId)) return;

        draggedDoc.style.left = e.position.x - dragOffset.x;
        draggedDoc.style.top = e.position.y - dragOffset.y;
    }

    private void OnPointerUp(PointerUpEvent e)
    {
        if (e.button == 0 && draggedDoc != null)
        {
            draggedDoc.ReleasePointer(e.pointerId);
            draggedDoc = null;
        }
    }

    private void TryStamp(VisualElement doc)
    {
        int docNumber = (int)doc.userData;
        int targetNumber = stampOrder[currentStampIndex];

        if (docNumber != targetNumber) return;

        doc.Q(className: "stamp-mark").style.display = DisplayStyle.Flex;

        currentStampIndex++;

        if (currentStampIndex >= documentCount)
            UITransitionChannel.TurnOn(nextScene);
        else
            UpdateTargetLabel();
    }

    private void UpdateTargetLabel()
    {
        targetLabel.text = $"Найти: №{stampOrder[currentStampIndex]}";
    }

    private static List<int> GenerateShuffled(int count)
    {
        var list = new List<int>(count);
        for (int i = 1; i <= count; i++)
            list.Add(i);

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }

        return list;
    }
}
