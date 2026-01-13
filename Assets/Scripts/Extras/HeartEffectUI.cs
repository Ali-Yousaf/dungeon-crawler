using UnityEngine;
using DG.Tweening;

public class HeartEffectScene : MonoBehaviour
{
    [Header("Heart Settings")]
    public Sprite[] heartSprites;         // Your 3 heart sprites
    public GameObject heartPrefab;        // Prefab with SpriteRenderer
    public float spawnInterval = 0.1f;    // How often hearts spawn (smaller = more hearts)
    public float floatDistance = 2f;      // How far hearts float up
    public float floatDuration = 3f;      // Time to float and fade
    public float spawnRadius = 1.5f;      // Radius around player
    public Vector2 horizontalDriftRange = new Vector2(-0.5f, 0.5f);
    public Vector2 scaleRange = new Vector2(0.5f, 1f);

    private Transform player;

    private void Start()
    {
        // Assume the script is attached to the player
        player = transform;
        InvokeRepeating(nameof(SpawnHeart), 0f, spawnInterval);
    }

    void SpawnHeart()
    {
        // Spawn position within a radius around player
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = player.position + new Vector3(randomCircle.x, randomCircle.y, 0f);

        // Instantiate heart prefab
        GameObject heart = Instantiate(heartPrefab, spawnPos, Quaternion.identity);

        // Set random sprite
        SpriteRenderer sr = heart.GetComponent<SpriteRenderer>();
        sr.sprite = heartSprites[Random.Range(0, heartSprites.Length)];

        // Slightly dull
        sr.color = new Color(1f, 1f, 1f, 0.8f);

        // Random scale
        float startScale = Random.Range(scaleRange.x, scaleRange.y);
        heart.transform.localScale = Vector3.one * startScale;

        // Random horizontal drift
        float horizontalOffset = Random.Range(horizontalDriftRange.x, horizontalDriftRange.y);

        // Random rotation
        float startRotation = Random.Range(-15f, 15f);
        float endRotation = startRotation + Random.Range(-45f, 45f);
        heart.transform.rotation = Quaternion.Euler(0, 0, startRotation);

        // Animate using DOTween
        Sequence seq = DOTween.Sequence();
        seq.Append(heart.transform.DOMoveY(spawnPos.y + floatDistance, floatDuration).SetEase(Ease.OutQuad));
        seq.Join(heart.transform.DOMoveX(spawnPos.x + horizontalOffset, floatDuration).SetEase(Ease.InOutSine));
        seq.Join(heart.transform.DOScale(startScale * 1.2f, floatDuration).SetEase(Ease.OutBack));
        seq.Join(heart.transform.DORotate(new Vector3(0, 0, endRotation), floatDuration, RotateMode.FastBeyond360));
        seq.Join(sr.DOFade(0f, floatDuration));
        seq.OnComplete(() => Destroy(heart));
    }
}
