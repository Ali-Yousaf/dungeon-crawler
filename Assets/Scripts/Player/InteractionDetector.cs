using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        interactionIcon.SetActive(false);
    }

    void Update()
    {
        if (interactableInRange != null)
        {
            interactionIcon.SetActive(interactableInRange.CanInteract());
        }

        if (Input.GetKeyDown(KeyCode.E) && interactableInRange != null)
        {
            if (interactableInRange.CanInteract())
            {
                audioManager.PlaySFX(audioManager.interactSound);
                interactableInRange.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(interactable.CanInteract());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
