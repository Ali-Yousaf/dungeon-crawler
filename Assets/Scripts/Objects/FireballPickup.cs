using UnityEngine;

public class FireballPickup : Pickup
{
    [TextArea]
    public string pickupMessage = "You picked up a Fireball. Press Q to shoot";

    protected override void OnPickup(GameObject player)
    {
        player.GetComponent<PlayerMovement>().canAttack = true;

        if (DialogManager.Instance != null)
            DialogManager.Instance.ShowDialog(pickupMessage);

        QuestManager.Instance.IncrementObjective("1"); //fireball id = 1
    }
}
