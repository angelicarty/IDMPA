using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    List<Quest> completedQuests = new List<Quest>();
    List<Quest> takenQuests = new List<Quest>();
    List<Quest> availableQuests = new List<Quest>();

    public void loadQuests()
    {
        //put every quest into the right list based on quest status
    }

    void questComplete(Quest currQuest)
    {
        takenQuests.Remove(currQuest);
        completedQuests.Add(currQuest);
    }


    public void killed(string monsterName)
    {
        Debug.Log(monsterName);
        //if monster name is in list of quest
        //add it to kill count
        if (takenQuests.Count > 0)
        {
            for (int i = 0; i < takenQuests.Count; i++)
            {
                if(monsterName.ToLower().Contains(takenQuests[i].monsterToKill.ToLower()))
                {
                    takenQuests[i].killCount++;
                }
            }
            
        }
    }

    public bool isQuestComplete(Quest currentQuest)
    {
        if (currentQuest.numberToKill <= currentQuest.killCount && currentQuest.questStatus.ToLower() == "taken")  //and object to collect
        {
            currentQuest.questStatus = "complete";
            questComplete(currentQuest);
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
        takenQuests.Add(quest);
        availableQuests.Remove(quest);
        Debug.Log("added quests: " + quest.monsterToKill);
    }

    public void dropQuest(Quest quest)
    {
        takenQuests.Remove(quest);
        availableQuests.Add(quest);
        Debug.Log("removed quests: " + quest.monsterToKill);
    }

}
