using UnityEngine;
using TMPro;

public class StatsUIManager : MonoBehaviour
{
    public static StatsUIManager Instance;

    [Header("UI References")]
    public GameObject statsPanel;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI keysText;
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI chestsText;
    public TextMeshProUGUI doorsText;
    public TextMeshProUGUI questsAcceptedText;
    public TextMeshProUGUI questsCompletedText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        RefreshUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (statsPanel != null)
                statsPanel.SetActive(!statsPanel.activeSelf);
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats instance not found!");
            return;
        }

        var stats = PlayerStats.Instance.stats;

        coinsText.text = $"<color=#FFD700>Coins:</color> {stats.coinsCollected}";
        keysText.text = $"<color=#00FFFF>Keys:</color> {stats.keysCollected}";
        enemiesText.text = $"<color=#FF4500>Enemies:</color> {stats.enemiesKilled}";
        chestsText.text = $"<color=#ADFF2F>Chests:</color> {stats.chestsOpened}";
        doorsText.text = $"<color=#1E90FF>Doors:</color> {stats.doorsOpened}";
        questsAcceptedText.text = $"<color=#FF69B4>Quests Accepted:</color> {stats.questAccepted}";
        questsCompletedText.text = $"<color=#BA55D3>Quests Completed:</color> {stats.questsCompleted}";
    }
}
