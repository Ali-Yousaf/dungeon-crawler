using UnityEngine;
using TMPro;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    public int totalKeys = 0;
    public TextMeshProUGUI keyText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddKey(int amount)
    {
        totalKeys += amount;
        UpdateUI();
    }

    public void UseKey()
    {
        totalKeys -= 1;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (keyText != null)
            keyText.text = totalKeys.ToString();
    }
}
