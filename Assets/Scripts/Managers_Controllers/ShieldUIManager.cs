using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ShieldUIManager : MonoBehaviour
{
    public static ShieldUIManager Instance;

    public int shieldCount = 0;
    public TextMeshProUGUI shieldText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddShield()
    {
        shieldCount++;
        UpdateUI();
    }

    public void UseShield()
    {
        shieldCount--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (shieldText != null)
            shieldText.text = shieldCount.ToString();
    }
}
