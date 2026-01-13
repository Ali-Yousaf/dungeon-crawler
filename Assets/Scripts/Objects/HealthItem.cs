using UnityEngine;

public class HealthItem : Pickup
{
    public int healAmount = 1;

    protected override void OnPickup(GameObject player)
    {
        PlayerHealth.Instance.Heal(healAmount);
    }
}
