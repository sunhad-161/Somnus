using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UITransitionScript : MonoBehaviour
{
    [SerializeField] private StyleSheet styleSheet;

    private VisualElement overlay;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.styleSheets.Add(styleSheet);
        overlay = new VisualElement();
        overlay.AddToClassList("transitionOverlay");
        root.Add(overlay);
    }

    private void OnEnable()
    {
        UITransitionChannel.UITransitionOnEvent += FadeOut;
    }

    private void OnDisable()
    {
        UITransitionChannel.UITransitionOnEvent -= FadeOut;
    }

    private void FadeOut(string sceneName)
    {
        overlay.AddToClassList("transitionOverlay--visible");
        overlay.RegisterCallbackOnce<TransitionEndEvent>(_ =>
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(sceneName);
        });
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        overlay.RemoveFromClassList("transitionOverlay--visible");
    }
}
