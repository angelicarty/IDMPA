using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    List<KillQuest> quests = new List<KillQuest>();

    public void killed(string monsterName)
    {
        //if monster name is in list of quest
        //add it to kill count
    }

    public void pickUpQuest(KillQuest quest)
    {

    }

    public void dropQuest(KillQuest quest)
    {

    }

}
