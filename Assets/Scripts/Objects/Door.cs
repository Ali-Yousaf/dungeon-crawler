using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool isOpened { get; private set; }
    public string doorID { get; private set; }

    private Animator animator;
    private Collider2D collider;

    // Optional: Number of keys required to open this door
    public int keysRequired = 1;

    private void Start()
    {
        if (string.IsNullOrEmpty(doorID))
            doorID = GlobalHelper.GenerateUniqueID(gameObject);

        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    public bool CanInteract()
    {
        return !isOpened && KeyManager.Instance.totalKeys >= keysRequired;
    }

    public void Interact()
    {
        if (!CanInteract())
        {
            DialogManager.Instance.ShowDialog("You need a key to open this door!");
            return;
        }

        OpenDoor();
    }

    private void OpenDoor()
    {
        PlayerStats.Instance.DoorOpened();

        animator.SetTrigger("Open");
        isOpened = true;

        if (collider != null)
            collider.enabled = false;

        KeyManager.Instance.UseKey();
    }
}
