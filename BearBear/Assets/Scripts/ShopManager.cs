using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    GameObject itemBeingSold;
    DialogueManager dialogueManager;
    RespondOptionsManager respondOptionsManager;
    InventoryManager inventoryManager;
    bool sellbuy;
    public Dialogue sellWhat;
    public Dialogue buyWhat;
    Sprite shopKeeper;

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
    }

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        respondOptionsManager = FindObjectOfType<RespondOptionsManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
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
        shopKeeper = sKeeper;
    }

    public void openShop()
    {
        dialogueManager.dialoguePrompt(welcomeDialogue, shopKeeper);
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
        Debug.Log("sell item");
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
        var itemProperties = item.GetComponent<ItemProperties>();
        if (itemProperties.value > 0) //item has value
        {
            if (inventoryManager.getCount(item) > 1) //if more than one item
            {
                //prompt how many to sell
                dialogueManager.dialoguePrompt(howMany);
                itemBeingSold = item;
                //counter.setActive(true);
            }
            else
            {
                //sell the one
                sold(item, itemProperties.value, 1);
            }
        }
        else //item that has no value can't be sold
        {
            dialogueManager.dialoguePrompt(cantSell);
        }
    }

    public void plusCounter()
    {
        buySellCount++;
        int count = inventoryManager.getCount(itemBeingSold);
        if (buySellCount > count)
        {
            buySellCount = count;
        }
    }

    public void minusCounter()
    {
        buySellCount--;
        if (buySellCount < 1)
        {
            buySellCount = 1;
        }
    }

    public void sellThisMany(GameObject item)
    {
        sold(item, item.GetComponent<ItemProperties>().value, buySellCount);
    }

    public void sold(GameObject item, int value, int count)
    {
        inventoryManager.addGold(value * count);
        inventoryManager.removeItem(item, count);
        
    }

    public void closeShop()
    {
        sellbuy = false;
    }
    /*
    private void Update()
    {
        if(Input.GetKeyDown("l"))
        {
            Debug.Log("pressed l");
            FindObjectOfType<RespondOptionsManager>().triggerShopReplies();
        }
    }
    */

}
