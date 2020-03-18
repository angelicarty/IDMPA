using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();


    public void killed(string monsterName)
    {
        //if monster name is in list of quest
        //add it to kill count
        if (quests != null)
        {
            if (monsterName.Contains("slime"))
            {
                for (int i = 0; i < quests.Count; i++)
                {
                    if(quests[i].monsterToKill.Contains("slime"))
                    {
                        quests[i].killCount++;
                    }
                }
            }
        }
    }

    public void pickUpQuest(Quest quest)
    {
        quests.Add(quest);
        Debug.Log("added quests: " + quest.monsterToKill);
    }

    public void dropQuest(Quest quest)
    {
        quests.Remove(quest);
        Debug.Log("removed quests: " + quest.monsterToKill);
    }

}
