using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<QuestProgress> activeQuests = new List<QuestProgress>();

    private void Start()
    {
        LoadQuests();
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddQuest(QuestProgress progress)
    {
        activeQuests.Add(progress);

        // Add to UI
        if (QuestUIController.Instance != null)
        {
            QuestUIController.Instance.AddQuest(progress);
            QuestUIController.Instance.UpdateQuestUI();
        }
    }

    // Increment progress for a given objectiveID
    public void IncrementObjective(string objectiveID, int amount = 1)
    {
        foreach (var quest in activeQuests)
        {
            foreach (var obj in quest.objectives)
            {
                if (obj.objectiveID == objectiveID)
                {
                    obj.currentAmount += amount;
                    if (obj.currentAmount > obj.requiredAmount)
                        obj.currentAmount = obj.requiredAmount;

                    Debug.Log($"Objective '{obj.description}' updated: {obj.currentAmount}/{obj.requiredAmount}");

                    // Update UI
                    if (QuestUIController.Instance != null)
                        QuestUIController.Instance.UpdateQuestUI();

                    if (!quest.completionTriggered && quest.isCompleted)
                    {
                        quest.completionTriggered = true;   // mark it so it only triggers once
                        QuestCompleteUI.Instance.ShowQuestComplete(quest.quest.questName);
                        PlayerStats.Instance.QuestCompleted();
                    }

                    SaveQuests();

                    return;
                }
            }
        }

        Debug.LogWarning("ObjectiveID not found: " + objectiveID);
    }

    public void SaveQuests()
    {
        QuestSaveSystem.Save(this);
    }

    public void LoadQuests()
    {
        QuestSaveSystem.Load(this);
    }
}
