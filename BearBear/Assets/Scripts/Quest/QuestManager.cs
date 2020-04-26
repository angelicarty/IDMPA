using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    List<Quest> completedQuests = new List<Quest>();
    public List<Quest> takenQuests = new List<Quest>();
    Quest currentQuest;
    public GameObject[] questList;
    public GameObject questUI;
    public GameObject nextArrow;
    public GameObject prevArrow;
    public GameObject questDescriptionBox;
    public GameObject questDescriptionNameBox;
    public GameObject questDescriptionDescBox;
    int pageNum;
    int selectedQuestNum = -1;
    string questTracker;
    public GameObject questDescriptionBoxTracker;
    public Stats playerStat;

    public void selectQuest(int num)
    {
        if ((num * (pageNum + 1)) == selectedQuestNum)
        {
            questDescriptionBox.SetActive(false);
            selectedQuestNum = -1;
        }
        else
        {
            questTracker = "";
            currentQuest = takenQuests[num * (pageNum + 1)];
            selectedQuestNum = num * (pageNum + 1);
            questDescriptionBox.SetActive(true);
            questDescriptionNameBox.GetComponent<UnityEngine.UI.Text>().text = takenQuests[num * (pageNum + 1)].questName;
            questDescriptionDescBox.GetComponent<UnityEngine.UI.Text>().text = takenQuests[num * (pageNum + 1)].questDescription;

            if(currentQuest.numberToKill != 0)
            {
                questTracker += currentQuest.monsterToKill + " : " + currentQuest.killCount + "/" + currentQuest.numberToKill;
            }

            questDescriptionBoxTracker.GetComponent<UnityEngine.UI.Text>().text = questTracker;

            //and then the inv tracker but that will need to wait till inv is done
        }
    }

    public void openQuestLog()
    {
        questUI.SetActive(true);
        pageNum = 0;
        displayQuestLog();
    }

    public void closeQuestLog()
    {
        questDescriptionBox.SetActive(false);
        questUI.SetActive(false);
    }

    void displayQuestLog()
    {
        for(int i =0; i<questList.Length;i++)
        {
            questList[i].SetActive(false);
        }
        for(int j=0; j<questList.Length;j++) 
        {
            if (j+(pageNum*questList.Length) < takenQuests.Count)
            {
                questList[j].SetActive(true);
                questList[j].GetComponentInChildren<UnityEngine.UI.Text>().text = takenQuests[j + (pageNum * questList.Length)].questName;
            }
        }
        if (takenQuests.Count > (pageNum+1)*questList.Length)
        {
            nextArrow.SetActive(true);
        }
        else
        {
            nextArrow.SetActive(false);
        }
        if(pageNum > 0)
        {
            prevArrow.SetActive(true);
        }
        else if(pageNum <= 0)
        {
            prevArrow.SetActive(false);
        }
    }

    public void nextPage()
    {
        pageNum++;
        displayQuestLog();
    }

    public void prevPage()
    {
        pageNum--;
        displayQuestLog();
    }

    public void loadQuests()
    {
        //put quest into the right list based on quest status from file
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
                if(monsterName.ToLower().Contains(takenQuests[i].monsterToKill.ToLower()) && takenQuests[i].killCount < takenQuests[i].numberToKill)
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
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool giveReward(Quest currQuest)
    {
        if(FindObjectOfType<InventoryManager>().giveItem(currQuest.itemReward, currQuest.itemRewardCount))
        {
            //give stat reward, if giving item reward can be done
            playerStat.ModMHP(currQuest.maxHP);
            playerStat.ModATK(currQuest.atk);
            playerStat.ModDEF(currQuest.def);
            playerStat.ModSATK(currQuest.satk);
            playerStat.ModSDEF(currQuest.sdef);
            playerStat.ModSPD(currQuest.spd);
            return true;
        }
        return false;

    }

    public void toGiveReward(Quest currQuest)
    {
        Debug.Log(currQuest.monsterToKill);
        if (giveReward(currQuest))
        {
            FindObjectOfType<DialogueManager>().rewardGiven();
            currQuest.questStatus = "complete";
            questComplete(currentQuest);
        }
    }

    public void pickUpQuest(Quest quest)
    {
        takenQuests.Add(quest);
        Debug.Log("added quests: " + quest.monsterToKill);
    }

    public void dropQuest()
    {
        takenQuests.Remove(currentQuest);
        currentQuest.questStatus = "available";
        currentQuest.killCount = 0;
        currentQuest.questGiver.GetComponent<DialogueTrigger>().questDropped();
        Debug.Log("removed quests: " + currentQuest.monsterToKill);
        displayQuestLog();
        questDescriptionBox.SetActive(false);
        selectedQuestNum = -1;
    }

}
