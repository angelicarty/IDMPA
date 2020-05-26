using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus { AVAILABLE, TAKEN, COMPLETED}
public class Quest : MonoBehaviour
{
    public string questGiverId;
    public string questDescription;
    public string questName;
    public string monsterToKill;
    public int numberToKill;
    public int killCount;
    public string objectToCollect;
    public int numberToCollect;
    public int collectionCount;
    public QuestStatus questStatus;

    public GameObject itemReward;
    public int itemRewardCount;
    //stats reward
    public int maxHP;
    public int atk;
    public int def;
    public int satk;
    public int sdef;
    public int spd;
}
