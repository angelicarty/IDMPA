using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();


    public void killed(string monsterName)
    {
        Debug.Log(monsterName);
        //if monster name is in list of quest
        //add it to kill count
        if (quests.Count > 0)
        {
            for (int i = 0; i < quests.Count; i++)
            {
                if(monsterName.ToLower().Contains(quests[i].monsterToKill.ToLower()))
                {
                    quests[i].killCount++;
                    Debug.Log(quests[i].monsterToKill);
                    Debug.Log(quests[i].killCount);
                }
            }
            
        }
    }

    public bool isQuestComplete(Quest currentQuest)
    {
        if (currentQuest.numberToKill <= currentQuest.killCount && currentQuest.questStatus.ToLower() == "taken")  //and object to collect
        {
            currentQuest.questStatus = "complete";
            return true;
        }
        else
        {
            return false;
        }
    }

    public void giveReward(Quest currQuest)
    {
        //bloop
        Debug.Log("reward given");
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
