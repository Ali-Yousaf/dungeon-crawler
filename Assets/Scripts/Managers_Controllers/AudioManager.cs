using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("--- AUDIO SOURCE ---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--- AUDIO CLIPS ---")]
    public AudioClip gameMusic;
    public AudioClip gameMusic2;
    public AudioClip endingMusic;
    public AudioClip dungeonMusic;
    public AudioClip finalMusic;
    public AudioClip interactSound;
    public AudioClip coinSound;
    public AudioClip fireballThrowSound;
    public AudioClip explosionSound;
    public AudioClip questCompleteSound;
    public AudioClip gameOverSound;
    public AudioClip playerHit;


    private void Start()
    {
        musicSource.clip = gameMusic2;
        musicSource.loop = true;
        musicSource.Play();

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Ending")
        {
            musicSource.clip = endingMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // TEMPLATE

    //AudioManager audioManager;
    
    //audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>(); 
    //audioManager.PlaySFX(audioManager.soundName);


}
