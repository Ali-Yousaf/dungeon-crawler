using System;
using System.Collections.Generic;

[Serializable]
public class QuestSaveData
{
    public List<QuestObjectiveSave> objectives = new List<QuestObjectiveSave>();
}

[Serializable]
public class QuestObjectiveSave
{
    public string questID;
    public string objectiveID;
    public int currentAmount;
}
