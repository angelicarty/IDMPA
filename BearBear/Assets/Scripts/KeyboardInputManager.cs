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
    bool gameMenu = false;
    public GameObject blur;
    public GameObject inGameMenu;

    MonstersController monstersController;
    QuestManager questManager;
    EquipmentManager equipmentManager;
    InventoryManager inventoryManager;
    CharacterStats characterStatsManager;
    DialogueManager dialogueManager;
    ShopManager shopManager;
    CharacterMove characterMove;
   

    private void Start()
    {
        monstersController = FindObjectOfType<MonstersController>();
        questManager = FindObjectOfType<QuestManager>();
        equipmentManager = FindObjectOfType<EquipmentManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        characterStatsManager = FindObjectOfType<CharacterStats>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        shopManager = FindObjectOfType<ShopManager>();
        characterMove = FindObjectOfType<CharacterMove>();
    }
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
                    questManager.CloseQuestLog();
                }
                else
                {
                    questOpen = true;
                    questManager.OpenQuestLog();
                }
            }

            if (Input.GetKeyDown("i"))
            {
                if(inventoryOpen)
                {
                    inventoryOpen = false;
                    inventoryManager.closeInventory();
                }
                else
                {
                    inventoryOpen = true;
                    inventoryManager.openInventory();
                }
            }

            if (Input.GetKeyDown("y"))
            {
                if (characterStats)
                {
                    characterStats = false;
                    characterStatsManager.hideStats();
                }
                else
                {
                    characterStats = true;
                    characterStatsManager.displayStats();
                }
            }

            if (Input.GetKeyDown("e"))
            {
                if (equipment)
                {
                    equipment = false;
                    equipmentManager.hide();
                }
                else
                {
                    equipment = true;
                    equipmentManager.show();
                }
            }

            if (Input.GetKeyDown("escape"))
            {
                if(shopDialogue)
                {
                    shopManager.pressedESC();
                    return;
                }
                if (!gameMenu)
                {
                    hideAllUi();
                    menu();
                    gameMenu = true;
                }
                else
                {
                    hideAllUi();
                    blur.SetActive(false);
                    enableCharacterMovement();
                    if(monstersController.isInMobArea())
                    {
                        monstersController.goingIntoMobArea();
                    }
                }
            }
        }
    }

    void menu()
    {
        blur.SetActive(true);
        inGameMenu.SetActive(true);
        disableCharacterMovement();
        if (monstersController.isInMobArea())
        {
            monstersController.pausesMobMovement();
        }
    }

    public void pressedSpace()
    {
        if (!shopDialogue)
        {
            dialogueManager.pressedSpace();
        }
        else
        {
            shopManager.pressedSpace();
        }
    }

    public void hideAllUi()
    {
        characterStats = false;
        characterStatsManager.hideStats();
        inventoryOpen = false;
        inventoryManager.closeInventory();
        questOpen = false;
        questManager.CloseQuestLog();
        equipment = false;
        equipmentManager.hide();
        gameMenu = false;
        inGameMenu.SetActive(false);
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
        characterMove.disableWalking();
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
        characterMove.enableWalking();
    }
}
