using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInventoryScript : MonoBehaviour
{
    [SerializeField] private StyleSheet styleSheet;

    private VisualElement bar;
    private Label notificationLabel;
    private Coroutine notificationCoroutine;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.styleSheets.Add(styleSheet);

        bar = new VisualElement();
        bar.AddToClassList("inventory-bar");
        root.Add(bar);

        notificationLabel = new Label();
        notificationLabel.AddToClassList("notification-label");
        notificationLabel.pickingMode = PickingMode.Ignore;
        notificationLabel.style.display = DisplayStyle.None;
        root.Add(notificationLabel);
    }

    private void OnEnable()
    {
        InventoryChannel.KeyAddedEvent += OnKeyAdded;
        InventoryChannel.KeyUsedEvent += OnKeyUsed;
        NotificationChannel.ShowEvent += OnNotification;
    }

    private void OnDisable()
    {
        InventoryChannel.KeyAddedEvent -= OnKeyAdded;
        InventoryChannel.KeyUsedEvent -= OnKeyUsed;
        NotificationChannel.ShowEvent -= OnNotification;
    }

    private void OnKeyAdded(string id)
    {
        var slot = new VisualElement();
        slot.name = $"key_{id}";
        slot.AddToClassList("key-slot");

        var label = new Label(id);
        label.AddToClassList("key-label");
        label.pickingMode = PickingMode.Ignore;
        slot.Add(label);

        bar.Add(slot);
    }

    private void OnKeyUsed(string id)
    {
        bar.Q($"key_{id}")?.RemoveFromHierarchy();
    }

    private void OnNotification(string message)
    {
        notificationLabel.text = message;
        notificationLabel.style.display = DisplayStyle.Flex;

        if (notificationCoroutine != null)
            StopCoroutine(notificationCoroutine);
        notificationCoroutine = StartCoroutine(HideNotification());
    }

    private IEnumerator HideNotification()
    {
        yield return new WaitForSeconds(2f);
        notificationLabel.style.display = DisplayStyle.None;
    }
}
