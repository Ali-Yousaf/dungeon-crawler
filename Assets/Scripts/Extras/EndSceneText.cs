using TMPro;
using UnityEngine;
using DG.Tweening;

public class EndSceneText : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject WedPanel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI playerNameText;

    [Header("Animation Settings")]
    [SerializeField] private float popDuration = 0.5f;   // Time to pop in/out
    [SerializeField] private float displayDuration = 2f; // How long panel stays before popping out

    private void Start()
    {
        // Set initial scale to zero
        WedPanel.SetActive(true);
        WedPanel.transform.localScale = Vector3.zero;

        // Update text
        string playerName = playerNameText.text;
        text.text = $"{playerName} WEDS PRINCESS";

        // Pop in
        WedPanel.transform.DOScale(1f, popDuration).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // After displayDuration, pop out
                WedPanel.transform.DOScale(0f, popDuration).SetEase(Ease.InBack)
                    .OnComplete(() => WedPanel.SetActive(false));
            });
    }
}
