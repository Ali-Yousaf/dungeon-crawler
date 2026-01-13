using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeOut(float duration, Action onComplete = null)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1f, duration).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    public void FadeIn(float duration, Action onComplete = null)
    {
        canvasGroup.DOFade(0f, duration).OnComplete(() =>
        {
            canvasGroup.blocksRaycasts = false;
            onComplete?.Invoke();
        });
    }
}
