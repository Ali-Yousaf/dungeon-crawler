using UnityEngine;
using DG.Tweening;

public abstract class Pickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupScalePop = 1.3f;
    public float pickupDuration = 0.15f;

    private bool pickedUp = false;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pickedUp) return;

        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.coinSound);

            pickedUp = true;
            OnPickup(collision.gameObject);
            PlayPickupAnimation();
        }
    }

    protected abstract void OnPickup(GameObject player);

    private void PlayPickupAnimation()
    {
        transform
            .DOScale(transform.localScale * pickupScalePop, pickupDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
