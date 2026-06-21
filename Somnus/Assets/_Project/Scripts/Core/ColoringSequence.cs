using System.Collections;
using UnityEngine;

public class ColoringSequence : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] coloredItems;
    [SerializeField] private SpriteRenderer coloredRoom;
    [SerializeField] private float fadeDuration = 0.8f;
    [SerializeField] private float pauseBetween = 1.0f;
    [SerializeField] private float initialDelay = 0.5f;

    private void Start()
    {
        foreach (var sr in coloredItems)
            SetAlpha(sr, 0f);
        SetAlpha(coloredRoom, 0f);

        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(initialDelay);

        foreach (var sr in coloredItems)
        {
            yield return FadeIn(sr);
            yield return new WaitForSeconds(pauseBetween);
        }

        yield return FadeIn(coloredRoom);
    }

    private IEnumerator FadeIn(SpriteRenderer sr)
    {
        if (sr == null) yield break;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(sr, Mathf.Clamp01(t / fadeDuration));
            yield return null;
        }
        SetAlpha(sr, 1f);
    }

    private static void SetAlpha(SpriteRenderer sr, float a)
    {
        if (sr == null) return;
        var c = sr.color;
        c.a = a;
        sr.color = c;
    }
}
