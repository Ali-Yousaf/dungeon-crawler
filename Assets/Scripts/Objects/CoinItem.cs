using UnityEngine;

public class CoinPickup : Pickup
{
    public int coinAmount;

    protected override void OnPickup(GameObject player)
    {
        CoinManager.Instance.AddCoins(coinAmount);
        QuestManager.Instance.IncrementObjective("3");

        PlayerStats.Instance.AddCoin();
    }
}
