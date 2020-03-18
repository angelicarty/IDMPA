using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public Quest quest;
    public void triggerQuest()
    {
        Debug.Log("monster to kill: " + quest.killing.monsterToKill);
    }
}
