using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour
{
    Dialogue[] nonQuestTakenDialogue;
    public Dialogue[] dialogues;

    public void questTaken(Dialogue[] newDialogue)
    {
        dialogues = newDialogue;
        FindObjectOfType<DialogueManager>().startDialogue(dialogues, gameObject);
    }

    public void questDropped()
    {
        dialogues = nonQuestTakenDialogue;
        FindObjectOfType<DialogueManager>().startDialogue(dialogues, gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            nonQuestTakenDialogue = dialogues;
            FindObjectOfType<DialogueManager>().startDialogue(dialogues, gameObject);
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<DialogueManager>().clearDialogues();
        }

    }


}