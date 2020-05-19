using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : MonoBehaviour
{
    bool characterChat = true;
    bool questOpen = false;
    bool inventoryOpen = false;
    public bool shopDialogue = false;
    bool characterStats = false;
    bool equipment = false;
    void Update()
    {
        if (characterChat)
        {
            if (Input.GetKeyDown("space"))
            {
                pressedSpace();
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

            if (Input.GetKeyDown("y"))
            {
                if (characterStats)
                {
                    characterStats = false;
                    FindObjectOfType<CharacterStats>().hideStats();
                }
                else
                {
                    characterStats = true;
                    FindObjectOfType<CharacterStats>().displayStats();
                }
            }

            if (Input.GetKeyDown("e"))
            {
                if (equipment)
                {
                    equipment = false;
                    FindObjectOfType<EquipmentManager>().hide();
                }
                else
                {
                    equipment = true;
                    FindObjectOfType<EquipmentManager>().show();
                }
            }

            if (Input.GetKeyDown("escape"))
            {
                hideAllUi();
            }
        }
    }

    public void pressedSpace()
    {
        if (!shopDialogue)
        {
            FindObjectOfType<DialogueManager>().pressedSpace();
        }
        else
        {
            FindObjectOfType<ShopManager>().pressedSpace();
        }
    }

    public void hideAllUi()
    {
        characterStats = false;
        FindObjectOfType<CharacterStats>().hideStats();
        inventoryOpen = false;
        FindObjectOfType<InventoryManager>().closeInventory();
        questOpen = false;
        FindObjectOfType<QuestManager>().closeQuestLog();
        equipment = false;
        FindObjectOfType<EquipmentManager>().hide();
    }

    public void shopDialogueToggleShop()
    {
        shopDialogue = true;
    }
    public void shopDialogueToggleDialogue()
    {
        shopDialogue = false;
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
