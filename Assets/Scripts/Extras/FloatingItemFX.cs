using UnityEngine;
using DG.Tweening;

public class FloatingItemFX : MonoBehaviour
{
    [Header("Floating")]
    public float floatDistance = 0.3f;
    public float floatDuration = 1.5f;

    [Header("Pulsing (Scale)")]
    public bool enablePulse = true;
    public float pulseScale = 1.1f;
    public float pulseDuration = 1.2f;

    [Header("Rotation")]
    public bool enableRotation = false;
    public float rotationSpeed = 360f;

    [Header("Glow Flash (Sprite Color)")]
    public bool enableGlow = false;
    public float glowIntensity = 1.3f;
    public float glowDuration = 0.8f;

    private Vector3 originalScale;
    private Vector3 startPos;
    private SpriteRenderer sprite;

    void Start()
    {
        startPos = transform.localPosition;
        originalScale = transform.localScale;
        sprite = GetComponent<SpriteRenderer>();

        PlayFloat();
        if (enablePulse) PlayPulse();
        if (enableRotation) PlayRotation();
        if (enableGlow && sprite != null) PlayGlow();
    }

    private void PlayFloat()
    {
        transform
            .DOLocalMoveY(startPos.y + floatDistance, floatDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void PlayPulse()
    {
        transform
            .DOScale(originalScale * pulseScale, pulseDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void PlayRotation()
    {
        transform
            .DORotate(new Vector3(0, 0, 360), rotationSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    private void PlayGlow()
    {
        sprite
            .DOColor(new Color(1f * glowIntensity, 1f * glowIntensity, 1f * glowIntensity), glowDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
