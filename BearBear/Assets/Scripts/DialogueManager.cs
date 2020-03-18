using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    
    public Queue<string> sentences;
    Dialogue[] AllDialogues;
    Dialogue currentDialogue;
    string talkerName;
    public GameObject chatBox;
    public GameObject dialogBox;
    public GameObject nameBox;
    public bool endChat;
    public bool typing;
    string sentence;
    bool skip;
    bool respondRequired;
    bool talking;
    bool waitingForReply;
    GameObject npc;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void clearDialogues()
    {
        AllDialogues = null;
        currentDialogue = null;
        talkerName = null;
        npc = null;
    }

    public void pressedSpace()
    {

        if(waitingForReply)
        {
            //no nothing
        }
        else if (typing)
        {
            skipTyping();
        }
        else if(talking)
        {
            DisplayNextSentence();
            
        }
        else if(AllDialogues.Length > 0)
        {
            startDialogue(AllDialogues, npc);
            DisplayNextSentence();
        }
        
    }
    public void startDialogue(Dialogue[] dialogues, GameObject npcRef)
    {
        AllDialogues = dialogues;
        endChat = false;
        npc = npcRef;

        currentDialogue = dialogues[Random.Range(0, dialogues.Length)];

        talkerName = currentDialogue.talkerName;
        if(currentDialogue.triggerOptions)
        {
            respondRequired = true;
        }
        else
        {
            respondRequired = false;
        }
        sentences.Clear();

        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        talking = true;
    }

    public void StartRespondDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        talkerName = currentDialogue.talkerName;
        sentences.Clear();
        respondRequired = false;
        endChat = false;

        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        talking = true;
        waitingForReply = false;
        FindObjectOfType<RespondOptionsManager>().isNotWaiting();
    }


    public void DisplayNextSentence()
    {
        dialogBox.SetActive(true);
        FindObjectOfType<KeyboardInputManager>().disableCharacterMovement();
        FindObjectOfType<MonstersController>().goingOutOfMobArea();
        nameBox.GetComponent<UnityEngine.UI.Text>().text = talkerName;

        if (sentences.Count == 0)
        {
            endChat = true;
            talking = false;
            endDialogue();
            return;
        }
        skip = false;
        typing = true;
        sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence());

    }
    IEnumerator TypeSentence()
    {
        chatBox.GetComponent<UnityEngine.UI.Text>().text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if(skip)
            {
                yield break;
            }
            chatBox.GetComponent<UnityEngine.UI.Text>().text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        typing = false;
    }

    public void skipTyping()
    {
        StopCoroutine(TypeSentence());
        skip = true;
        typing = false;
        chatBox.GetComponent<UnityEngine.UI.Text>().text = sentence;
    }

    void endDialogue()
    {
        if (respondRequired)
        {
            FindObjectOfType<RespondOptionsManager>().acceptReplies(currentDialogue.optionreplies, npc);
            currentDialogue = null;
            waitingForReply = true;
            FindObjectOfType<RespondOptionsManager>().isWaiting();
        }
        else
        {
            //chatBox.GetComponent<UnityEngine.UI.Text>().text = "BYE"; //don't clear text if respondrequired
            //nameBox.GetComponent<UnityEngine.UI.Text>().text = "endchat";
            dialogBox.SetActive(false);
            FindObjectOfType<KeyboardInputManager>().enableCharacterMovement();
            FindObjectOfType<MonstersController>().goingIntoMobArea();
        }
    }
}