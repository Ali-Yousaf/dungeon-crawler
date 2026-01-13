using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeartUIController : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Heart Images")]
    public Image[] heartImages;

    [Header("Animation Settings")]
    public float popScale = 1.3f;
    public float popDuration = 0.2f;

    private int lastHealth = -1;
    private int lastMaxHearts = -1;

    private void Awake()
    {
        foreach (var img in heartImages)
            img.transform.localScale = Vector3.one;
    }

    public void UpdateHearts(int currentHealth, int maxHearts)
    {
        if (lastHealth == -1)
            lastHealth = currentHealth;

        for (int i = 0; i < heartImages.Length; i++)
        {
            bool isFull = i < currentHealth;
            bool shouldBeVisible = i < maxHearts;

            heartImages[i].enabled = shouldBeVisible;
            heartImages[i].sprite = isFull ? fullHeart : emptyHeart;
        }

        // Animate difference
        if (currentHealth < lastHealth)
        {
            // Damage animation
            for (int i = currentHealth; i < lastHealth; i++)
            {
                if (i < heartImages.Length)
                    PlayDamageAnimation(heartImages[i]);
            }
        }

        else if (currentHealth > lastHealth)
        {
            // Heal animation
            for (int i = lastHealth; i < currentHealth; i++)
            {
                if (i < heartImages.Length)
                    PlayHealAnimation(heartImages[i]);
            }
        }

        lastHealth = currentHealth;
        lastMaxHearts = maxHearts;
    }

    //DOTWEEN ANIMATIONS
    private void PlayHealAnimation(Image img)
    {
        img.transform.DOKill();
        img.transform.localScale = Vector3.one;

        img.transform
            .DOScale(popScale, popDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                img.transform.DOScale(1f, 0.15f).SetEase(Ease.InOutSine);
            });
    }

    private void PlayDamageAnimation(Image img)
    {
        img.transform.DOKill();
        img.transform.localScale = Vector3.one;

        img.transform
            .DOScale(0.7f, popDuration * 0.6f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                img.transform.DOScale(1f, popDuration).SetEase(Ease.OutBack);
            });
    }
}
