using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue[] dialogues;



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
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