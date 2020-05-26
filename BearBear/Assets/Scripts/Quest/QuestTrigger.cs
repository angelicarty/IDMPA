using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public GameObject quest;
    public Sprite speakerSprite;
    public Dialogue[] questTakenDialogue;
    public Dialogue[] questCompleteDialogue;
    public Dialogue[] invFullDialogue;

    public string id;//ID used to tie quests to givers, used because you can't save static references
    public bool taken;//taken flag, used to avoid editing prefab object

    public void TriggerQuest()
    {
        if (!taken)
        {
            gameObject.GetComponent<DialogueTrigger>().questTaken(questTakenDialogue);
            FindObjectOfType<QuestManager>().PickUpQuest(quest);
            taken = true;
        }
    }
    public void InvIsFull()
    {
        FindObjectOfType<DialogueManager>().startDialogue(invFullDialogue, gameObject, speakerSprite);
    }
}
