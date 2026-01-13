using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int itemID;
    public string itemName;

    private bool isPickedUp = false;

    public virtual void PickUp()
    {
        if (isPickedUp) return;

        isPickedUp = true;

        Sprite itemIcon = GetComponent<SpriteRenderer>().sprite;
        ItemPickupUIController.Instance.ShowItemPickup(itemName, itemIcon);
    }
}
