using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public Quest quest;
    public Dialogue[] questTakenDialogue;
    public Dialogue[] questCompleteDialogue;
    public Dialogue[] invFullDialogue;

    public void triggerQuest()
    {
        if (quest.questStatus == "available")
        {
            quest.questStatus = "taken";
            gameObject.GetComponent<DialogueTrigger>().questTaken(questTakenDialogue);
            FindObjectOfType<QuestManager>().pickUpQuest(quest);
        }
    }
    public void invIsFull()
    {
        FindObjectOfType<DialogueManager>().startDialogue(invFullDialogue, gameObject);
    }
}
