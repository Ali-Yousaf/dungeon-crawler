using TMPro;
using UnityEngine;
using DG.Tweening;

public class QuestCompleteUI : MonoBehaviour
{
    public static QuestCompleteUI Instance;

    [Header("UI Components")]
    public RectTransform panel;          
    public TextMeshProUGUI questCompleteText;
    public float displayDuration = 2f;
    public float slideDistance = 100f;    
    public float slideTime = 0.5f;       

    private Vector2 originalPosition;

    AudioManager audioManager;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        originalPosition = panel.anchoredPosition;
        panel.gameObject.SetActive(false);

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>(); 

    }

    public void ShowQuestComplete(string questName)
    {
        audioManager.PlaySFX(audioManager.questCompleteSound);

        panel.gameObject.SetActive(true);
        questCompleteText.text = $"Quest Completed: {questName}";

        panel.anchoredPosition = originalPosition + new Vector2(0, slideDistance);
        panel.DOKill();

        panel.DOAnchorPos(originalPosition, slideTime).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // After displayDuration, slide back up
                DOVirtual.DelayedCall(displayDuration, () =>
                {
                    panel.DOAnchorPos(originalPosition + new Vector2(0, slideDistance), slideTime)
                         .SetEase(Ease.InBack)
                         .OnComplete(() => panel.gameObject.SetActive(false));
                });
            });
    }
}
