using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UITransitionScript : MonoBehaviour
{
    [SerializeField] private StyleSheet styleSheet;
    private const float FadeDuration = 0.8f;

    private VisualElement overlay;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.styleSheets.Add(styleSheet);
        overlay = new VisualElement();
        overlay.AddToClassList("transitionOverlay");
        overlay.pickingMode = PickingMode.Ignore;
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
        StartCoroutine(FadeRoutine(sceneName));
    }

    private IEnumerator FadeRoutine(string sceneName)
    {
        overlay.AddToClassList("transitionOverlay--visible");
        yield return new WaitForSeconds(FadeDuration);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        overlay.RemoveFromClassList("transitionOverlay--visible");
    }
}
