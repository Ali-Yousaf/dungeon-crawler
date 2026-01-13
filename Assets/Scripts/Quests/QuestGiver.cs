using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour, IInteractable
{
    [Header("Quest To Give")]
    public Quest quest;

    public Sprite portraitImage;

    private bool isGiven = false;

    public void Interact()
    {
        if (!CanInteract()) return;

        QuestProgress newProgress = new QuestProgress(quest);

        QuestManager.Instance.AddQuest(newProgress);
        PlayerStats.Instance.QuestAccepted();

        isGiven = true;

        DialogManager.Instance.ShowDialog("Quest Added! Check your Quest Log.", portraitImage);
        Debug.Log($"Quest given: {quest.questName}");
    }

    public bool CanInteract()
    {
        return !isGiven;
    }
}
