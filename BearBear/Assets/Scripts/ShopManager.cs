using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //opens the shop ui
    //welcome, how may i help you
    //sell item/buy item

    //sell item
    //opens inv 

    Dialogue welcomeDialogue;
    Dialogue noMoneyDialogue;
    Dialogue noSpaceDialogue;
    Dialogue goodbyeDialogue;
    GameObject[] itemsForSale;

    public Dialogue howMany;
    public Dialogue cantSell;

    bool inShop = false;

    public GameObject counter;
    int buySellCount;
    public GameObject counterText;

    DialogueManager dialogueManager;
    RespondOptionsManager respondOptionsManager;
    InventoryManager inventoryManager;
    KeyboardInputManager keyboardInputManager;

    bool sellbuy;
    public Dialogue sellWhat;
    public Dialogue buyWhat;
    public Dialogue changeYourMind;
    public Dialogue youGotNothing;
    public Dialogue hereIsYourChange;

    Sprite shopKeeper;
    GameObject itemToSell;

    public void pressedSpace()
    {
        Debug.Log("in shop: " + inShop);
        if(sellbuy)
        {
            //nada
        }
        else if(inShop)
        {
            openShop();
        }
        else
        {
            dialogueManager.pressedSpace();
        }
    }

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        respondOptionsManager = FindObjectOfType<RespondOptionsManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        keyboardInputManager = FindObjectOfType<KeyboardInputManager>();
    }

    public void triggerShop(Dialogue welcomeDia,Dialogue noMuns,Dialogue noSpace,Dialogue bye ,GameObject[] items, Sprite sKeeper)
    {
        welcomeDialogue = welcomeDia;
        itemsForSale = items;
        noMoneyDialogue = noMuns;
        noSpaceDialogue = noSpace;
        goodbyeDialogue = bye;
        inShop = true;
        howMany.talkerName = welcomeDia.talkerName;
        cantSell.talkerName = welcomeDia.talkerName;
        changeYourMind.talkerName = welcomeDia.talkerName;
        youGotNothing.talkerName = welcomeDia.talkerName;
        sellWhat.talkerName = welcomeDia.talkerName;
        hereIsYourChange.talkerName = welcomeDia.talkerName;
        shopKeeper = sKeeper;
    }

    public void openShop()
    {
        dialogueManager.dialoguePromptWithSprite(welcomeDialogue, shopKeeper);
        respondOptionsManager.triggerShopReplies();
        sellbuy = true;
    }
    public void resetShop()
    {
        welcomeDialogue = null;
        itemsForSale = null;
        noMoneyDialogue = null;
        noSpaceDialogue = null;
        goodbyeDialogue = null;
        sellbuy = false;
    }

    public bool isPlayerInShop()
    {
        return inShop;
    }

    public void triggerSellItem()
    {
        keyboardInputManager.disableCharacterMovement();
        sellbuy = true;
        if (inventoryManager.isInvEmpty())
        {
            dialogueManager.dialoguePromptWithSprite(youGotNothing, shopKeeper);
        }
        else
        {


            inventoryManager.openInventory();
            dialogueManager.dialoguePromptWithSprite(sellWhat, shopKeeper);
        }
    }

    public void triggerBuyItem()
    {
        Debug.Log("buy item");
    }

    public void outFromShop()
    {
        inShop = false;
    }

    public void sellThis(GameObject item)
    {
        itemToSell = item;
        var itemProperties = item.GetComponent<ItemProperties>();
        if (itemProperties.value > 0) //item has value
        {
            if (inventoryManager.getCount(item) > 1) //if more than one item
            {
                //prompt how many to sell
                dialogueManager.dialoguePromptWithSprite(howMany, shopKeeper);
                counter.SetActive(true);
            }
            else
            {
                //sell the one
                sold(item, itemProperties.value, 1);
                sellbuy = false;
            }
        }
        else //item that has no value can't be sold
        {
            dialogueManager.dialoguePromptWithSprite(cantSell, shopKeeper);
        }
    }

    public void plusCounter()
    {
        buySellCount++;
        int count = inventoryManager.getCount(itemToSell);
        if (buySellCount > count)
        {
            buySellCount = count;
        }
        counterText.GetComponent<Text>().text = buySellCount.ToString();
    }

    public void minusCounter()
    {
        buySellCount--;
        if (buySellCount < 0)
        {
            buySellCount = 0;
        }
        counterText.GetComponent<Text>().text = buySellCount.ToString();
    }
    

    public void sellThisMany()
    {
        counter.SetActive(false);
        if (buySellCount > 0)
        {
            sold(itemToSell, itemToSell.GetComponent<ItemProperties>().value, buySellCount);
            dialogueManager.dialoguePromptWithSprite(hereIsYourChange, shopKeeper);
            sellbuy = false;
        }
        else
        {
            dialogueManager.dialoguePromptWithSprite(changeYourMind, shopKeeper);
            closeShop();
            sellbuy = false;
        }

    }

    public void sold(GameObject item, int value, int count)
    {
        inventoryManager.addGold(value * count);
        inventoryManager.removeItem(item, count);
        
    }

    public void closeShop()
    {
        sellbuy = false;
        dialogueManager.dialoguePromptWithSprite(goodbyeDialogue, shopKeeper);
        keyboardInputManager.enableCharacterMovement();
    }


}
