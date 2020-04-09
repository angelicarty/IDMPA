﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : MonoBehaviour
{
    bool characterChat = true;
    bool questOpen = false;
    bool inventoryOpen = false;
    void Update()
    {
        if (characterChat)
        {
            if (Input.GetKeyDown("space"))
            {
                FindObjectOfType<DialogueManager>().pressedSpace();
            }

            if(Input.GetKeyDown("q"))
            {
                if (questOpen)
                {
                    questOpen = false;
                    FindObjectOfType<QuestManager>().closeQuestLog();
                }
                else
                {
                    questOpen = true;
                    FindObjectOfType<QuestManager>().openQuestLog();
                }
            }

            if (Input.GetKeyDown("i"))
            {
                if(inventoryOpen)
                {
                    inventoryOpen = false;
                    FindObjectOfType<InventoryManager>().closeInventory();
                }
                else
                {
                    inventoryOpen = true;
                    FindObjectOfType<InventoryManager>().openInventory();
                }
            }
        }
    }

    public void disableCharacterMovement() //prevents character from moving
    {
        FindObjectOfType<CharacterMove>().disableWalking();
    }

    public void disableChat()
    {
        characterChat = false;
    }

    public void enableChat()
    {
        characterChat = true;
    }

    public void enableCharacterMovement() //reenable character to move
    {
        FindObjectOfType<CharacterMove>().enableWalking();
    }
}
