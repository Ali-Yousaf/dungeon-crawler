using UnityEngine;

public class KeyPickup : Pickup
{
    public int keyLevel = 1;

    protected override void OnPickup(GameObject player)
    {
        KeyManager.Instance.AddKey(keyLevel);
        QuestManager.Instance.IncrementObjective("4");

        PlayerStats.Instance.AddKey();
    }
}
