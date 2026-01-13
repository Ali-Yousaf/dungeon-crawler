using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public StatsData stats = new StatsData();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadStats();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        stats.coinsCollected++;
        PlayerPrefs.SetInt("CoinsCollected", stats.coinsCollected);
    }

    public void AddKey()
    {
        stats.keysCollected++;
        PlayerPrefs.SetInt("KeysCollected", stats.keysCollected);
    }

    public void EnemyKilled()
    {
        stats.enemiesKilled++;
        PlayerPrefs.SetInt("EnemiesKilled", stats.enemiesKilled);
    }

    public void ChestOpened()
    {
        stats.chestsOpened++;
        PlayerPrefs.SetInt("ChestsOpened", stats.chestsOpened);
    }

    public void DoorOpened()
    {
        stats.doorsOpened++;
        PlayerPrefs.SetInt("DoorsOpened", stats.doorsOpened);
    }

    public void QuestAccepted()
    {
        stats.questAccepted++;
        PlayerPrefs.SetInt("QuestAccepted", stats.questAccepted);
    }

    public void QuestCompleted()
    {
        stats.questsCompleted++;
        PlayerPrefs.SetInt("QuestsCompleted", stats.questsCompleted);
    }

    private void LoadStats()
    {
        stats.coinsCollected = PlayerPrefs.GetInt("CoinsCollected", 0);
        stats.keysCollected = PlayerPrefs.GetInt("KeysCollected", 0);
        stats.enemiesKilled = PlayerPrefs.GetInt("EnemiesKilled", 0);
        stats.chestsOpened = PlayerPrefs.GetInt("ChestsOpened", 0);
        stats.doorsOpened = PlayerPrefs.GetInt("DoorsOpened", 0);
        stats.questAccepted = PlayerPrefs.GetInt("QuestAccepted", 0);
        stats.questsCompleted = PlayerPrefs.GetInt("QuestsCompleted", 0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteAll();
        stats = new StatsData();
    }

}
