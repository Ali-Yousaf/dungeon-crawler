using UnityEngine;
using System.Collections.Generic;

public static class QuestSaveSystem
{
    private const string SaveKey = "QuestSaveData";

    public static void Save(QuestManager manager)
    {
        QuestSaveData data = new QuestSaveData();

        foreach (var quest in manager.activeQuests)
        {
            foreach (var obj in quest.objectives)
            {
                data.objectives.Add(new QuestObjectiveSave
                {
                    questID = quest.quest.questID,
                    objectiveID = obj.objectiveID,
                    currentAmount = obj.currentAmount
                });
            }
        }

        string json = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();

        Debug.Log("Quests Saved:\n" + json);
    }

    public static void Load(QuestManager manager)
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            Debug.Log("No quest save data found.");
            return;
        }

        string json = PlayerPrefs.GetString(SaveKey);
        QuestSaveData data = JsonUtility.FromJson<QuestSaveData>(json);

        Debug.Log("Loaded Quest Data:\n" + json);

        // Clear current active quests
        manager.activeQuests.Clear();

        // Load all quests from Resources/Quests
        foreach (var quest in Resources.LoadAll<Quest>("Quests"))
        {
            // Find saved objectives for this quest
            var savedObjectives = data.objectives.FindAll(o => o.questID == quest.questID);
            if (savedObjectives.Count > 0)
            {
                QuestProgress progress = new QuestProgress(quest);

                // Restore objective amounts
                foreach (var obj in progress.objectives)
                {
                    var savedObj = savedObjectives.Find(o => o.objectiveID == obj.objectiveID);
                    if (savedObj != null)
                        obj.currentAmount = savedObj.currentAmount;
                }

                manager.activeQuests.Add(progress);
            }
        }

        // Sync UI
        if (QuestUIController.Instance != null)
        {
            QuestUIController.Instance.questProgresses = new List<QuestProgress>(manager.activeQuests);
            QuestUIController.Instance.UpdateQuestUI();
        }
    }
}
