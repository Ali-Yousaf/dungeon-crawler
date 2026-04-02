using UnityEngine;

public class ShieldItem : Pickup
{
    protected override void OnPickup(GameObject player)
    {
        ShieldUIManager.Instance.AddShield();
    }
}
