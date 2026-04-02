using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [SerializeField] private GameObject gameOverPanel;

    public int maxHearts = 5;
    public int currentHearts = 5;

    public float iFrameDuration = 0.5f;
    public bool isInvincible;

    private HeartUIController heartUI;

    AudioManager audioManager;

    private void Awake()
    {
        Instance = this;

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
           TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
           Heal(1);
        }
    }

    private void Start()
    {
        heartUI = FindFirstObjectByType<HeartUIController>();
        heartUI.UpdateHearts(currentHearts, maxHearts);
    }

    //Take Damage
    public void TakeDamage(int amount)
    {
        audioManager.PlaySFX(audioManager.playerHit);

        if (isInvincible) return;

        currentHearts = Mathf.Clamp(currentHearts - amount, 0, maxHearts);
        heartUI.UpdateHearts(currentHearts, maxHearts);

        if (currentHearts <= 0)
            Die();
        
        else
            StartCoroutine(IFrames());
    }

    //Heal
    public void Heal(int amount)
    {
        currentHearts = Mathf.Clamp(currentHearts + amount, 0, maxHearts);
        heartUI.UpdateHearts(currentHearts, maxHearts);
    }

    //Invincibility
    private IEnumerator IFrames()
    {
        isInvincible = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        for (int i = 0; i < 5; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.05f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }

        isInvincible = false;
    }

    private void Die()
    {
        audioManager.PlaySFX(audioManager.gameOverSound);
        gameOverPanel.SetActive(true);

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Destroy(gameObject, 1.5f);
        Debug.Log("Player died!");
    }
}
