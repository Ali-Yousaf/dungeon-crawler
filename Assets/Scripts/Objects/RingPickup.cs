using UnityEngine;

public class RingPickup : Pickup
{
    public bool ringCollected = false;

    protected override void OnPickup(GameObject player)
    {
        //ending sequence
        QuestManager.Instance.IncrementObjective("0");
        ringCollected = true;


    }
}
