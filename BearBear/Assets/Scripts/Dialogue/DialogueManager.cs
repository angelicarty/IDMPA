﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject blur;
    public Queue<string> sentences;
    public Dialogue invFullDialogue;
    Dialogue[] AllDialogues;
    Dialogue currentDialogue;
    string talkerName;
    public GameObject chatBox;
    public GameObject dialogBox;
    public GameObject nameBox;
    public GameObject speakerSpriteBox;
    public bool endChat;
    public bool typing;
    string sentence;
    bool skip;
    bool respondRequired;
    bool talking;
    bool waitingForReply;
    GameObject npc;
    bool giveReward;
    public QuestManager qm;
    Quest currentQuest;
    bool canChat;
    Sprite speakerSprite;
    public Sprite empty;

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
        giveReward = false;
        respondRequired = false;
        speakerSpriteBox.GetComponent<Image>().sprite = empty;
    }

    public void cantTalk()
    {
        canChat = false;
    }

    public void canTalk()
    {
        canChat = true;
    }

    public void rewardGiven()
    {
        giveReward = false;
    }

    public void pressedSpace()
    {
        if(!canChat)
        {
            //no
        }
        else if(waitingForReply)
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
        else if(AllDialogues != null)
        {
            startDialogue(AllDialogues, npc, speakerSprite);
            DisplayNextSentence();
            
        }
        
        
    }
    public void startDialogue(Dialogue[] dialogues, GameObject npcRef,Sprite speaker)
    {
        giveReward = false;
        speakerSprite = speaker;
        speakerSpriteBox.GetComponent<Image>().sprite = speakerSprite;
        var questPresent = npcRef.GetComponent<QuestTrigger>();
        if (questPresent == null)
        {
            //there's no quest trigger

            AllDialogues = dialogues;
            currentDialogue = dialogues[Random.Range(0, dialogues.Length)];

            endChat = false;
            npc = npcRef;
            talkerName = currentDialogue.talkerName;
            if (currentDialogue.triggerOptions)
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
        else
        {
            //there is quest trigger
            currentQuest = GetTakenQuest(questPresent.id);

            if (currentQuest == null)
            {
                //there is a quest trigger but the quest aint taken
                AllDialogues = dialogues;
                currentDialogue = dialogues[Random.Range(0, dialogues.Length)];

                talkerName = currentDialogue.talkerName;
                if (currentDialogue.triggerOptions)
                {
                    respondRequired = true;
                }
                else
                {
                    respondRequired = false;
                }
            }
            else if (currentQuest.questStatus == QuestStatus.TAKEN)
            {

                //if quest conditions are fufilled, 
                if (qm.IsQuestComplete(currentQuest))
                {
                    if (FindObjectOfType<InventoryManager>().isInvFull()) //if inventory is full
                    {
                        currentDialogue = invFullDialogue;
                    }
                    else
                    {
                        giveReward = true;
                        AllDialogues = questPresent.questCompleteDialogue;
                        currentDialogue = AllDialogues[Random.Range(0, dialogues.Length)];
                    }
                }
                else
                {
                    AllDialogues = questPresent.questTakenDialogue;
                    currentDialogue = AllDialogues[Random.Range(0, dialogues.Length)];
                }

            }
            else if (currentQuest.questStatus == QuestStatus.COMPLETED)
            {
                AllDialogues = questPresent.questCompleteDialogue;
                currentDialogue = AllDialogues[Random.Range(0, dialogues.Length)];
            }
            else //there is a quest trigger but the quest aint taken
            {
                //should never get here
                Debug.LogError("Dialogue manager thinks the quest is availabe, but the player already has it");
            }

            endChat = false;
            npc = npcRef;
            talkerName = currentDialogue.talkerName;
            sentences.Clear();

            foreach (string sentence in currentDialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            talking = true;
        }


    }

    public Quest GetTakenQuest(string giverId)
    {
        //returns game object of taken quest from quest giver's id
        Quest[] questLog = qm.GetComponentsInChildren<Quest>();
        foreach (Quest quest in questLog)
        {
            if (quest.questGiverId == giverId)
            {
                return quest;
            }
        }
        //if no id
        return null;
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
        blur.SetActive(true);
        dialogBox.SetActive(true);
        FindObjectOfType<KeyboardInputManager>().disableCharacterMovement();
        FindObjectOfType<MonstersController>().pausesMobMovement();
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
            yield return new WaitForSecondsRealtime(0.02f);
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

    public void endDialogue()
    {
        if (giveReward)
        {
            FindObjectOfType<QuestManager>().ToGiveReward(currentQuest);
        }
        if (respondRequired)
        {
            var respondOptionsManager = FindObjectOfType<RespondOptionsManager>();
            respondOptionsManager.acceptReplies(currentDialogue.optionreplies, npc);
            currentDialogue = null;
            waitingForReply = true;
            respondOptionsManager.isWaiting();
        }
        else
        {
            dialogBox.SetActive(false);
            blur.SetActive(false);
            FindObjectOfType<KeyboardInputManager>().enableCharacterMovement();
            //if (FindObjectOfType<MonstersController>().isInMobArea())
            //{
                FindObjectOfType<MonstersController>().goingIntoMobArea();
            //}
        }
    }


    public void dialoguePrompt(Dialogue dialogue)
    {
        clearDialogues();
        currentDialogue = dialogue;
        canChat = true;
        giveReward = false;
        endChat = false;
        talkerName = currentDialogue.talkerName;
        sentences.Clear();

        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        talking = true;
        DisplayNextSentence();
    }

    public void dialoguePromptWithSprite(Dialogue dialogue, Sprite speaker)
    {
        if (typing)
        {
            if (dialogue.talkerName == currentDialogue.talkerName)
            {
                for (int i = 0; i < dialogue.sentences.Length; i++)
                {
                    currentDialogue.sentences[currentDialogue.sentences.Length] = dialogue.sentences[0];
                }
            }
        }
        else
        {
            clearDialogues();
            currentDialogue = dialogue;
            canChat = true;
            giveReward = false;
            endChat = false;
            Debug.Log(dialogue.talkerName);
            talkerName = dialogue.talkerName;
            speakerSprite = speaker;
            speakerSpriteBox.GetComponent<Image>().sprite = speakerSprite;
            sentences.Clear();

            foreach (string sentence in currentDialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            talking = true;
            DisplayNextSentence();
        }
    }

}