using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespondOptionsManager : MonoBehaviour
{
    public GameObject respondBox, button1, button2;
    public Image[] buttonImages;
    List<Dialogue> replies = new List<Dialogue>();
    Dialogue currReply, currReply2;
    bool waitingForReply;
    bool option1,option2;
    GameObject questGiver;
    public GameObject button1Selected, button2Selected;

    private int selectedAction = -1;
    bool quest;


    void resetThis()
    {
        replies = null;
        waitingForReply = false;
        option1 = false;
        option2 = false;
        replies = new List<Dialogue>();
        quest = false;
    }
    //below this line is shop stuff

    public void triggerShopReplies()
    {

        respondBox.SetActive(true);
        waitingForReply = true;
        button1.GetComponentInChildren<UnityEngine.UI.Text>().text = "buy stuff";
        button2.GetComponentInChildren<UnityEngine.UI.Text>().text = "sell stuff";
    }


    //below this line is mostly quest stuff

    public void acceptReplies(OptionReply[] OptionReplies, GameObject questGiverRef)
    {
        quest = true;
        respondBox.SetActive(true);

        questGiver = questGiverRef;

        option1 = OptionReplies[0].acceptQuest;
        option2 = OptionReplies[1].acceptQuest;
        
        button1.GetComponentInChildren<UnityEngine.UI.Text>().text = OptionReplies[0].option; 
        button2.GetComponentInChildren<UnityEngine.UI.Text>().text = OptionReplies[1].option; 



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
        if (quest)
        {
            var dialogueManager = FindObjectOfType<DialogueManager>();
            switch (chosenOPtion)
            {
                case 0:
                    dialogueManager.StartRespondDialogue(replies[0]);
                    if (option1)
                    {
                        questGiver.GetComponent<QuestTrigger>().triggerQuest();
                    }
                    respondBox.SetActive(false);
                    resetThis();
                    break;
                case 1:
                    dialogueManager.StartRespondDialogue(replies[1]);
                    if (option2)
                    {
                        questGiver.GetComponent<QuestTrigger>().triggerQuest();
                    }
                    respondBox.SetActive(false);
                    resetThis();
                    break;
                default:
                    break;
            }
        }
        else
        {
            //else, is shop
            var shopManager = FindObjectOfType<ShopManager>().GetComponent<ShopManager>();
            switch (chosenOPtion)
            {
                case 0:
                    shopManager.triggerBuyItem();
                    respondBox.SetActive(false);
                    resetThis();
                    break;
                case 1:
                    shopManager.triggerSellItem();
                    respondBox.SetActive(false);
                    resetThis();
                    break;
                default:
                    break;
            }
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
