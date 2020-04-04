using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Quest
{
    public GameObject questGiver;
    public string questDescription;
    public string questName;
    public string monsterToKill;
    public int numberToKill;
    public int killCount;
    public string objectToCollect;
    public int numberToCollect;
    public int collectionCount;
    public string questStatus;
}

