using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupUIController : MonoBehaviour
{
    public static ItemPickupUIController Instance { get; private set; }

    public GameObject popUpPrefab;
    public int maxPopups = 5;
    public float popUpDuration = 3f;

    private readonly Queue<GameObject> activePopups = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
    }

    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup = Instantiate(popUpPrefab, transform);
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName;

        Image itemImage = newPopup.transform.Find("ItemIcon")?.GetComponent<Image>();

        if(itemImage)
        {
            itemImage.sprite = itemIcon;
        }

        activePopups.Enqueue(newPopup);

        if(activePopups.Count > maxPopups)
        {
            Destroy(activePopups.Dequeue());
        }

        StartCoroutine(FadeOutAndDestroy(newPopup));
    }

    private IEnumerator FadeOutAndDestroy(GameObject popUp)
    {
        yield return new WaitForSeconds(popUpDuration);

        if (popUp == null)
            yield break;

        CanvasGroup canvasGroup = popUp.GetComponentInChildren<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = popUp.AddComponent<CanvasGroup>();

        float duration = 1f; 

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            if (canvasGroup == null)
                yield break;

            float alpha = Mathf.Lerp(1f, 0f, t / duration);
            canvasGroup.alpha = alpha;

            yield return null;
        }

        canvasGroup.alpha = 0f;

        Destroy(popUp);
    }
}
