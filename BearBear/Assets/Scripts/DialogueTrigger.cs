using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue[] dialogues;
    Dialogue dialogue;
    bool playerInDialogueTrigger;
    bool talking;
    bool typing;

    public GameObject dialogueBox;
    
    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pleaseWork();
        }
            
    }
    */

    public void pleaseWork()
    {
        if (IsThisTyping())
        {
            FindObjectOfType<DialogueManager>().skipTyping();
            if (FindObjectOfType<DialogueManager>().endChat)
            {
                talking = false;
            }
        }
        else if (talking)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            if (FindObjectOfType<DialogueManager>().endChat)
            {
                talking = false;
            }
        }
        else
        {
            if (playerInDialogueTrigger == true) //show the talking chat bubble vfx here to show you can talk to him
            {
                talking = true;
                dialogue = dialogues[Random.Range(0, dialogues.Length)];
                TriggerDialogue();
            }

        }
        
        
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }

    public bool IsThisTyping()
    {
        return FindObjectOfType<DialogueManager>().typing;
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            //dialogueBox.GetComponent<UnityEngine.UI.Text>().text = "hewwo? pwease i'm in the box";
            playerInDialogueTrigger = true;
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInDialogueTrigger = false;
        }

    }


}