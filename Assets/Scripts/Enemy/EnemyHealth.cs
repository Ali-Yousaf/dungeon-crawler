using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.CompilerServices;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public Slider healthSlider;

    [Header("Animation")]
    public float smoothSpeed = 0.2f;

    private Animator animator;
    private SpriteRenderer sR;
    
    private bool isFlashing = false;
    private bool isDead = false;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>(); 

        animator = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        audioManager.PlaySFX(audioManager.explosionSound);

        StartCoroutine(ChangeColorOnHit());

        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        StartCoroutine(AnimateHealthBar());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator ChangeColorOnHit()
    {
        if (isFlashing) yield break;
        isFlashing = true;

        Color ogColor = sR.color;
        sR.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        sR.color = ogColor;
        isFlashing = false;
    }


    IEnumerator AnimateHealthBar()
    {
        float startValue = healthSlider.value;
        float endValue = currentHealth;
        float time = 0;

        while (time < smoothSpeed)
        {
            time += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(startValue, endValue, time / smoothSpeed);
            yield return null;
        }

        healthSlider.value = endValue;
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        QuestManager.Instance.IncrementObjective("10");

        PlayerStats.Instance.EnemyKilled();


        //Camera shake
        print("Camera Shake Called");
        CameraShake.Instance.Shake();

        animator.SetTrigger("Die");

        GetComponent<EnemyDropper>().DropItem();
        
        Canvas hb = GetComponentInChildren<Canvas>();
        hb.gameObject.SetActive(false);
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
