using UnityEngine;
using System.Collections;

public class BounceEffect : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceScale = 1.2f;  
    public float bounceTime = 0.1f;    

    private Vector3 originalScale;
    private bool isBouncing = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void PlayBounce()
    {
        if (!isBouncing)
            StartCoroutine(Bounce());
    }

    private IEnumerator Bounce()
    {
        isBouncing = true;

        yield return ScaleTo(originalScale * bounceScale, bounceTime);

        yield return ScaleTo(originalScale, bounceTime);

        isBouncing = false;
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 target, float duration)
    {
        Vector3 start = transform.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }
    }
}
