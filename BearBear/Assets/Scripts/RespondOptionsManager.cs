using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespondOptionsManager : MonoBehaviour
{
    public GameObject respondBox, button1, button2;
    public Image[] buttonImages;
    List<Dialogue> replies = new List<Dialogue>();
    Dialogue currReply, currReply2;// = new Dialogue();
    bool waitingForReply;
    bool aQuest,bQuest;
    GameObject questGiver;
    public GameObject button1Selected, button2Selected;

    //public Dialogue testing;
    //public Dialogue testing2;

    private int selectedAction = -1;

    public void acceptReplies(OptionReply[] OptionReplies, GameObject questGiverRef)
    {
        respondBox.SetActive(true);

        questGiver = questGiverRef;
        if(OptionReplies[0].acceptQuest) 
        {
            aQuest = true;
        }
        if(OptionReplies[1].acceptQuest)
        {
            bQuest = true;
        }
        
        button1.GetComponentInChildren<UnityEngine.UI.Text>().text = OptionReplies[0].option; //this works
        button2.GetComponentInChildren<UnityEngine.UI.Text>().text = OptionReplies[1].option; //this works



        currReply = new Dialogue();
        currReply.talkerName = OptionReplies[0].talkerName;
        currReply.sentences = OptionReplies[0].reply;
        currReply.triggerOptions = false;
        replies.Add(currReply);

        currReply2 = new Dialogue();
        currReply2.talkerName = OptionReplies[1].talkerName;
        currReply2.sentences = OptionReplies[1].reply;
        currReply2.triggerOptions = false;
        replies.Add(currReply2);
        
    }

    public void isWaiting()
    {
        waitingForReply = true;
    }

    public void isNotWaiting()
    {
        waitingForReply = false;
    }

    public void pickAReply(int chosenOPtion)
    {
        switch(chosenOPtion)
        {
            case 0:
                FindObjectOfType<DialogueManager>().StartRespondDialogue(replies[0]);
                if (aQuest)
                {
                    questGiver.GetComponent<QuestTrigger>().triggerQuest();
                }
                respondBox.SetActive(false);
                break;
            case 1:
                FindObjectOfType<DialogueManager>().StartRespondDialogue(replies[1]);
                if(bQuest)
                {
                    questGiver.GetComponent<QuestTrigger>().triggerQuest();
                }
                respondBox.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void highlightButton(int index)
    {
        if (index > -1)
        {
            if (index == 0)
            {
                button1Selected.SetActive(true);
                button2Selected.SetActive(false);
            }
            if (index == 1)
            {
                button2Selected.SetActive(true);
                button1Selected.SetActive(false);
            }
            //buttonImages[index].color = Color.blue;
        }

        
    }

    void Update()
    {
        if (waitingForReply)
        {
            if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && selectedAction > -1)
            {
                pickAReply(selectedAction);
                selectedAction = -1;
                highlightButton(selectedAction);
                return;
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedAction = 0;
                highlightButton(selectedAction);
                return;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedAction = 1;
                highlightButton(selectedAction);
                return;
            }
            else if (selectedAction < 0)
            {
                selectedAction = 0;
                highlightButton(selectedAction);
            }
        }
    }



}
