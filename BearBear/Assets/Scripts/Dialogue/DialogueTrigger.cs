using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour
{
    Dialogue[] nonQuestTakenDialogue;
    public Dialogue[] dialogues;
    bool inTrigger;

    public void questTaken(Dialogue[] newDialogue)
    {
        dialogues = newDialogue;
        FindObjectOfType<DialogueManager>().startDialogue(dialogues, gameObject);
    }



    public void questDropped()
    {
        dialogues = nonQuestTakenDialogue;
        if(inTrigger)
        {
            FindObjectOfType<DialogueManager>().startDialogue(dialogues, gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inTrigger = true;
            var dialogueManager = FindObjectOfType<DialogueManager>();
            nonQuestTakenDialogue = dialogues;
            dialogueManager.startDialogue(dialogues, gameObject);
            dialogueManager.canTalk();
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inTrigger = false;
            var dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.cantTalk();
            dialogueManager.clearDialogues();
        }

    }


}