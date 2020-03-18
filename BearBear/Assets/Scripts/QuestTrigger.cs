using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public KeyCode dropQuestTestingButton;
    public Quest quest;
    public Dialogue[] questTakenDialogue;

    public void triggerQuest()
    {
        if (quest.questStatus == "available")
        {
            quest.questStatus = "taken";
            gameObject.GetComponent<DialogueTrigger>().questTaken(questTakenDialogue);
            FindObjectOfType<QuestManager>().pickUpQuest(quest);
        }
    }

    private void Update()
    {
        //testing purpose
        if(Input.GetKeyDown(dropQuestTestingButton))
        {
            quest.questStatus = "available";
            GetComponent<DialogueTrigger>().questDropped();
            FindObjectOfType<QuestManager>().dropQuest(quest);
        }
    }
}
