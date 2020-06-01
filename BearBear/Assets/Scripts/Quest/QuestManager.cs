using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    List<GameObject> completedQuests = new List<GameObject>();
    public List<GameObject> takenQuests = new List<GameObject>();
    GameObject currentQuest;
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

    public void SelectQuest(int num)
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
            questDescriptionNameBox.GetComponent<UnityEngine.UI.Text>().text = takenQuests[num * (pageNum + 1)].GetComponent<Quest>().questName;
            questDescriptionDescBox.GetComponent<UnityEngine.UI.Text>().text = takenQuests[num * (pageNum + 1)].GetComponent<Quest>().questDescription;

            if(currentQuest.GetComponent<Quest>().numberToKill != 0)
            {
                questTracker += currentQuest.GetComponent<Quest>().monsterToKill + " : " + currentQuest.GetComponent<Quest>().killCount + "/" + currentQuest.GetComponent<Quest>().numberToKill;
            }
            if(currentQuest.GetComponent<Quest>().numberToCollect != 0)
            {
                questTracker += Environment.NewLine + currentQuest.GetComponent<Quest>().objectToCollect.GetComponent<ItemProperties>().name + " : " + FindObjectOfType<InventoryManager>().getCount(currentQuest.GetComponent<Quest>().objectToCollect)
                    + "/" + currentQuest.GetComponent<Quest>().numberToCollect;
            }

            questDescriptionBoxTracker.GetComponent<UnityEngine.UI.Text>().text = questTracker;

            //and then the inv tracker but that will need to wait till inv is done
        }
    }

    public void OpenQuestLog()
    {
        questUI.SetActive(true);
        pageNum = 0;
        DisplayQuestLog();
    }

    public void CloseQuestLog()
    {
        questDescriptionBox.SetActive(false);
        questUI.SetActive(false);
    }

    void DisplayQuestLog()
    {
        for(int i = 0; i < questList.Length;i++)
        {
            questList[i].SetActive(false);
        }
        for(int j=0; j<questList.Length;j++) 
        {
            if (j+(pageNum*questList.Length) < takenQuests.Count)
            {
                questList[j].SetActive(true);
                questList[j].GetComponentInChildren<UnityEngine.UI.Text>().text = takenQuests[j + (pageNum * questList.Length)].GetComponent<Quest>().questName;
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

    public void NextPage()
    {
        pageNum++;
        DisplayQuestLog();
    }

    public void PrevPage()
    {
        pageNum--;
        DisplayQuestLog();
    }

    public void LoadQuests()
    {
        //put quest into the right list based on quest status from file
    }

    void QuestComplete(GameObject currQuest)
    {
           //TODO:
        takenQuests.Remove(currQuest);
        completedQuests.Add(currQuest);
    }


    public void Killed(string monsterName)
    {
        Debug.Log(monsterName);
        //if monster name is in list of quest
        //add it to kill count
        if (takenQuests.Count > 0)
        {
            for (int i = 0; i < takenQuests.Count; i++)
            {
                if(monsterName.ToLower().Contains(takenQuests[i].GetComponent<Quest>().monsterToKill.ToLower()) && takenQuests[i].GetComponent<Quest>().killCount < takenQuests[i].GetComponent<Quest>().numberToKill)
                {
                    takenQuests[i].GetComponent<Quest>().killCount++;
                }
            }
            
        }
    }

    public bool IsQuestComplete(Quest currentQuest)
    {
        if (currentQuest.numberToKill <= currentQuest.killCount && currentQuest.numberToCollect <= FindObjectOfType<InventoryManager>().getCount(currentQuest.objectToCollect) &&
            currentQuest.questStatus == QuestStatus.TAKEN)  
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GiveReward(Quest currQuest)
    {
        if(FindObjectOfType<InventoryManager>().questGiveItem(currQuest.itemReward, currQuest.itemRewardCount))
        {
            Debug.Log("1");
            //remove quest object
            FindObjectOfType<InventoryManager>().removeItem(currQuest.objectToCollect, currQuest.numberToCollect);
            //give stat reward, if giving item reward can be done
            Debug.Log("2");
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

    public void ToGiveReward(Quest currQuest)
    {
        if (GiveReward(currQuest))
        {
            FindObjectOfType<DialogueManager>().rewardGiven();
            currQuest.questStatus = QuestStatus.COMPLETED;
            QuestComplete(currentQuest);
        }
        else
        {
            //TODO: currentQuest.GetComponent<Quest>().questGiver.gameObject.GetComponent<QuestTrigger>().invIsFull();
        }
    }

    public void PickUpQuest(GameObject quest)
    {
        GameObject temp = Instantiate(quest, gameObject.transform);
        temp.GetComponent<Quest>().questStatus = QuestStatus.TAKEN;
        temp.name = temp.name.Replace("(Clone)", "");
        takenQuests.Add(temp); 
        Debug.Log("added quests: " + temp.GetComponent<Quest>().monsterToKill);
    }

    public void DropQuest()
    {
        
        takenQuests.Remove(currentQuest);
        Quest quest = currentQuest.GetComponent<Quest>();
        QuestTrigger[] givers = FindObjectsOfType<QuestTrigger>();
        foreach (QuestTrigger giver in givers)
        {
            if (giver.id == quest.questGiverId)
            {
                giver.taken = false;
                giver.GetComponent<DialogueTrigger>().questDropped();
                break;
            }
        }


        Debug.Log("removed quests: " + quest.monsterToKill);
        DisplayQuestLog();
        questDescriptionBox.SetActive(false);
        selectedQuestNum = -1;
        Destroy(currentQuest);
    }

    //saving
    public string[] GetAllTakenIds()
    {
        string[] output = new string[takenQuests.Count];
        int i = 0;
        foreach (GameObject quest in takenQuests)
        {
            output[i] = quest.name.Replace("(Clone)", "");
            i++;
        }
        return output;
    }

    public string[] GetAllCompletedIds()
    {
        string[] output = new string[completedQuests.Count];
        int i = 0;
        foreach (GameObject quest in completedQuests)
        {
            output[i] = quest.name.Replace("(Clone)", "");
            i++;
        }
        return output;
    }

    public int[] GetKillQuestProgress()
    {
        //TODO: check if quest is mob or item
        int[] output = new int[takenQuests.Count];
        int i = 0;
        foreach (GameObject quest in takenQuests)
        {
            output[i] = quest.GetComponent<Quest>().killCount;
            i++;
        }
        return output;
    }

    public int[] GetItemQuestProgress()
    {
        //TODO: check if quest is mob or item
        int[] output = new int[takenQuests.Count];
        int i = 0;
        foreach (GameObject quest in takenQuests)
        {
            output[i] = quest.GetComponent<Quest>().collectionCount;
            i++;
        }
        return output;
    }

    public void InitQuests(string[] taken, string[] complete, int[] killProgress, int[] itemProgress)
    {
        GameObject temp;
        List<GameObject> takenObj = new List<GameObject>();
        List<GameObject> completeObj = new List<GameObject>();
        QuestList list = FindObjectOfType<QuestList>();
        //get quest objects from ids
        for (int i = 0; i < list.Quests.Length; i++)
        {
            for (int j = 0; j < taken.Length; j++)
            {
                if (list.Quests[i].name == taken[j])
                {
                    takenObj.Add(list.Quests[i]);
                } 
            }
            for (int j = 0; j < complete.Length; j++)
            {
                if (list.Quests[i].name == complete[j])
                {
                    completeObj.Add(list.Quests[i]);
                }
            }
        }


        for (int i = 0; i < takenObj.Count; i++)
        {
            temp = Instantiate(takenObj[i], gameObject.transform);
            temp.name = temp.name.Replace("(Clone)", "");
            temp.GetComponent<Quest>().killCount = killProgress[i];
            temp.GetComponent<Quest>().collectionCount = itemProgress[i];
            temp.GetComponent<Quest>().questStatus = QuestStatus.TAKEN;
            takenQuests.Add(temp);
            temp = null;
        }

        for (int i = 0; i < completeObj.Count; i++)
        {
            temp = Instantiate(completeObj[i], gameObject.transform);
            temp.GetComponent<Quest>().questStatus = QuestStatus.COMPLETED;
            completedQuests.Add(temp);
            temp = null;
        }

        //updates quest givers
        QuestTrigger[] givers = FindObjectsOfType<QuestTrigger>();
        foreach (QuestTrigger giver in givers)
        {
            foreach (GameObject quest in takenQuests)
            {
                if (giver.id == quest.GetComponent<Quest>().questGiverId)
                {
                    giver.GetComponent<DialogueTrigger>().questTaken(giver.questTakenDialogue);
                    giver.taken = true;
                    break;
                }
            }

            foreach (GameObject quest in completedQuests)
            {
                if (giver.id == quest.GetComponent<Quest>().questGiverId)
                {
                    giver.GetComponent<DialogueTrigger>().questTaken(giver.questTakenDialogue);
                    giver.taken = true;
                    break;
                }
            }
        }
    }
}
