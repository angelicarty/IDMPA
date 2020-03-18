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
    //public Dialogue testing;
    //public Dialogue testing2;

    private int selectedAction;

    public void acceptReplies(OptionReply[] OptionReplies)
    {
        respondBox.SetActive(true);

        Debug.Log(OptionReplies[0].talkerName);
        Debug.Log(OptionReplies[0].reply[0]);
        
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

    public void pickAReply(int chosenOPtion)
    {
        switch(chosenOPtion)
        {
            case 0:
                Debug.Log("choosen one");
                FindObjectOfType<DialogueManager>().StartRespondDialogue(replies[0]);
                respondBox.SetActive(false);
                break;
            case 1:
                Debug.Log("choosen 2");
                FindObjectOfType<DialogueManager>().StartRespondDialogue(replies[1]);
                respondBox.SetActive(false);
                break;
            default:
                Debug.Log("bloop");
                break;
        }
    }

    public void highlightButton(int index)
    {
        //copied from battleui, highlights the currently selected button
        //TODO: move this to ui class
        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[i].color = Color.white;
        }
        if (index > -1)
        {
            buttonImages[index].color = Color.blue;
        }
    }

    void Update()
    {
        if (respondBox.active)
        {
            if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && selectedAction > -1)
            {
                switch (selectedAction)
                {
                    case 0://TODO: trigger "yes"

                        break;
                    case 1://TODO: trigger "no"

                        break;
                }
                selectedAction = -1;
                highlightButton(selectedAction);
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedAction = 0;
                highlightButton(selectedAction);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedAction = 1;
                highlightButton(selectedAction);
            }
        }
    }

}
