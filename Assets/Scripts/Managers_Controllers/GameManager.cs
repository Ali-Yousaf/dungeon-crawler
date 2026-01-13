using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void ResetStatsButton()
    {
        PlayerStats.Instance.ResetStats();
    }
}
