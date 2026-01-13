using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUIController : MonoBehaviour
{
    public static QuestUIController Instance;

    public Transform questListContent;
    public GameObject questEntryPrefab;
    public GameObject objectiveTextPrefab;

    public List<QuestProgress> questProgresses = new List<QuestProgress>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Do not auto-add quests
        UpdateQuestUI();
    }

    public void UpdateQuestUI()
    {
        foreach (Transform child in questListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var quest in questProgresses)
        {
            GameObject entry = Instantiate(questEntryPrefab, questListContent);
            TMP_Text questNameText = entry.transform.Find("questNameText").GetComponent<TMP_Text>();
            Transform objectiveList = entry.transform.Find("objList");

            questNameText.text = quest.quest.name;

            foreach (var objective in quest.objectives)
            {
                GameObject objTextGO = Instantiate(objectiveTextPrefab, objectiveList);
                TMP_Text objText = objTextGO.GetComponent<TMP_Text>();
                objText.text = $"{objective.description} ({objective.currentAmount} / {objective.requiredAmount})";
            }
        }
    }

    public void AddQuest(QuestProgress progress)
    {
        questProgresses.Add(progress);
    }
}
