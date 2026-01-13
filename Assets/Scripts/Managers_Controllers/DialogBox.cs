using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [Header("UI References - No Portrait")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;

    [Header("UI References - With Portrait")]
    public GameObject dialogPanelWithPortrait;
    public TextMeshProUGUI dialogTextWithPortrait;
    public Image portraitImage;

    [Header("Animation Settings")]
    public float popDuration = 0.25f;
    public float fadeDuration = 0.2f;
    public float popScale = 1.15f;

    private CanvasGroup cgNoPortrait;
    private CanvasGroup cgPortrait;
    private RectTransform rtNoPortrait;
    private RectTransform rtPortrait;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        dialogPanel.SetActive(true);
        dialogPanelWithPortrait.SetActive(true);

        rtNoPortrait = dialogPanel.GetComponent<RectTransform>();
        cgNoPortrait = dialogPanel.GetComponent<CanvasGroup>();

        rtPortrait = dialogPanelWithPortrait.GetComponent<RectTransform>();
        cgPortrait = dialogPanelWithPortrait.GetComponent<CanvasGroup>();

        HideAllImmediate();
    }

    private void HideAllImmediate()
    {
        dialogPanel.SetActive(false);
        dialogPanelWithPortrait.SetActive(false);

        cgNoPortrait.alpha = 0f;
        cgPortrait.alpha = 0f;

        rtNoPortrait.localScale = Vector3.zero;
        rtPortrait.localScale = Vector3.zero;
    }

    // SHOW DIALOG WITHOUT PORTRAIT
    public void ShowDialog(string message)
    {
        HideAllImmediate();

        dialogText.text = message;
        dialogPanel.SetActive(true);

        rtNoPortrait.localScale = Vector3.zero;
        cgNoPortrait.alpha = 0f;

        rtNoPortrait.DOScale(popScale, popDuration).SetEase(Ease.OutBack);
        cgNoPortrait.DOFade(1f, fadeDuration);
        rtNoPortrait.DOScale(1f, 0.15f).SetDelay(popDuration).SetEase(Ease.OutQuad);
    }

    // SHOW DIALOG WITH PORTRAIT
    public void ShowDialog(string message, Sprite portrait)
    {
        HideAllImmediate();

        dialogTextWithPortrait.text = message;

        if (portraitImage != null)
        {
            portraitImage.sprite = portrait;
            portraitImage.enabled = (portrait != null);
        }

        dialogPanelWithPortrait.SetActive(true);

        rtPortrait.localScale = Vector3.zero;
        cgPortrait.alpha = 0f;

        rtPortrait.DOScale(popScale, popDuration).SetEase(Ease.OutBack);
        cgPortrait.DOFade(1f, fadeDuration);
        rtPortrait.DOScale(1f, 0.15f).SetDelay(popDuration).SetEase(Ease.OutQuad);
    }

    public void HideDialog()
    {
        cgNoPortrait.DOFade(0f, fadeDuration);
        cgPortrait.DOFade(0f, fadeDuration);

        rtNoPortrait.DOScale(0f, popDuration).SetEase(Ease.InBack);
        rtPortrait.DOScale(0f, popDuration).SetEase(Ease.InBack);

        DOVirtual.DelayedCall(popDuration, () =>
        {
            dialogPanel.SetActive(false);
            dialogPanelWithPortrait.SetActive(false);
        });
    }
}