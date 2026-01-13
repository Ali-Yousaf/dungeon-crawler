using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSequence : MonoBehaviour
{
    RingPickup ring;

    private void Awake()
    {
        ring = FindFirstObjectByType<RingPickup>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(ring.ringCollected == true)
            {
                SceneManager.LoadScene("Ending");
            }
        }
    }
}
