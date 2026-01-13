using UnityEngine;
using Unity.Cinemachine;

public class DungeonEntrance : MonoBehaviour, IInteractable
{
    [Header("Teleport Settings")]
    public Transform teleportTarget;         
    public bool useFade = true;               
    public float fadeDuration = 0.5f;

    [Header("Map Transition")]
    [SerializeField] PolygonCollider2D mapBoundry;
    CinemachineConfiner2D confiner;

    public bool isEntrance = true;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>(); 
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    public bool CanInteract() => true;

    public void Interact()
    {
        Debug.Log("Teleporting player into dungeon...");

        if(isEntrance)
        {
            DialogManager.Instance.ShowDialog("Entering Dungeon...");
            audioManager.PlayMusic(audioManager.dungeonMusic);
        }


        else
        {
            DialogManager.Instance.ShowDialog("Exiting Dungeon...");
            audioManager.PlayMusic(audioManager.gameMusic2);
        }

        confiner.BoundingShape2D = mapBoundry;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure your player has the tag 'Player'.");
            return;
        }

        if (useFade)
            FadeTeleport(player);

        else
            player.transform.position = teleportTarget.position;
    }

    private void FadeTeleport(GameObject player)
    {
        ScreenFader.Instance.FadeOut(fadeDuration, () =>
        {
            player.transform.position = teleportTarget.position;
            ScreenFader.Instance.FadeIn(fadeDuration);
        });
    }
}
