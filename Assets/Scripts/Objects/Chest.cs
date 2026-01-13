using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool isOpened { get; private set; }
    public string chestID { get; private set; }

    public GameObject itemPrefab;
    public Sprite chestOpenSprite;

    void Start()
    {
        chestID ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

    // ========== INTERACTION SYSTEM ==========

    public bool CanInteract()
    {
        return !isOpened;
    }

    public void Interact()
    {
        if (!CanInteract())
            return;

        OpenChest();
    }

    // ========== CHEST LOGIC ==========

    private void OpenChest()
    {
        SetOpened(true);

        PlayerStats.Instance.ChestOpened();

        if (itemPrefab)
        {
            GameObject droppedItem = Instantiate(
                itemPrefab,
                transform.position + new Vector3(0, -1.5f, 0),
                Quaternion.identity
            );

            droppedItem.GetComponent<BounceEffect>()?.PlayBounce();
        }
    }

    public void SetOpened(bool opened)
    {
        isOpened = opened;

        if (isOpened)
        {
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sprite = chestOpenSprite;
        }
    }
}
