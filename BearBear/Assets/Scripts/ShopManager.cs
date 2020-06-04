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
    Dialogue buyingDialogue;
    GameObject[] itemsForSale;

    public Dialogue howMany;
    public Dialogue cantSell;

    bool inShop = false;
    bool pleaseEndConvo = false;
    bool boughtSold = false;
    bool isBuying = false;
    bool isSelling = false;

    public GameObject shopUI;
    public GameObject[] shopSlot;
    public GameObject counter;
    int buySellCount;
    public GameObject counterText;
    public bool startShop;

    DialogueManager dialogueManager;
    RespondOptionsManager respondOptionsManager;
    InventoryManager inventoryManager;
    KeyboardInputManager keyboardInputManager;

    bool waiting;
    public Dialogue sellWhat;
    public Dialogue buyWhat;
    public Dialogue changeYourMind;
    public Dialogue youGotNothing;
    public Dialogue hereIsYourChange;

    Sprite shopKeeper;
    GameObject itemToSell;
    GameObject itemToBuy;

    public void pressedSpace()
    {
        Debug.Log("please end convo:" + pleaseEndConvo);
        if(pleaseEndConvo && inShop && !waiting)
        {
            dialogueManager.endDialogue();
            inventoryManager.closeInventory();
            pleaseEndConvo = false;
            isBuying = false;
            isSelling = false;
            return;
        }
        if(waiting && inShop && !pleaseEndConvo)
        {/*
            if(counter.activeSelf)
            {
                ThisMany();
            }*/
            //do nothing
            return;
        }
        if (inShop && !waiting && !pleaseEndConvo)
        {
            startShop = true;
            openShop();
            return;
        }
    }

    public void pressedESC()
    {
        if (waiting && inShop && !pleaseEndConvo)
        {
            dialogueManager.dialoguePromptWithSprite(changeYourMind, shopKeeper);
            pleaseEndConvo = true;
            waiting = false;
            shopUI.SetActive(false);
            inventoryManager.closeInventory();
            isBuying = false;
            isSelling = false;
            return;
        }
    }

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        respondOptionsManager = FindObjectOfType<RespondOptionsManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        keyboardInputManager = FindObjectOfType<KeyboardInputManager>();
    }

    public void triggerShop(Dialogue welcomeDia,Dialogue noMuns,Dialogue noSpace,Dialogue bye,Dialogue buy ,GameObject[] items, Sprite sKeeper)
    {
        isBuying = false;
        isSelling = false;
        welcomeDialogue = welcomeDia;
        itemsForSale = items;
        noMoneyDialogue = noMuns;
        noSpaceDialogue = noSpace;
        goodbyeDialogue = bye;
        buyingDialogue = buy;
        inShop = true;
        howMany.talkerName = welcomeDia.talkerName;
        cantSell.talkerName = welcomeDia.talkerName;
        changeYourMind.talkerName = welcomeDia.talkerName;
        youGotNothing.talkerName = welcomeDia.talkerName;
        sellWhat.talkerName = welcomeDia.talkerName;
        buyWhat.talkerName = welcomeDia.talkerName;
        hereIsYourChange.talkerName = welcomeDia.talkerName;
        shopKeeper = sKeeper;
    }

    public void openShop()
    {
        isBuying = false;
        isSelling = false;
        dialogueManager.dialoguePromptWithSprite(welcomeDialogue, shopKeeper);
        respondOptionsManager.triggerShopReplies();
        pleaseEndConvo = true;
    }
    public void resetShop()
    {
        welcomeDialogue = null;
        itemsForSale = null;
        noMoneyDialogue = null;
        noSpaceDialogue = null;
        goodbyeDialogue = null;
        waiting = false;
        pleaseEndConvo = false;
        boughtSold = false;
        isBuying = false;
        isSelling = false;
    }

    public bool isPlayerInShop()
    {
        return inShop;
    }

    public void triggerSellItem()
    {
        isSelling = true;
        keyboardInputManager.disableCharacterMovement();
        if (inventoryManager.isInvEmpty())
        {
            dialogueManager.dialoguePromptWithSprite(youGotNothing, shopKeeper);
            waiting = false;
            pleaseEndConvo = true;
        }
        else
        {
            waiting = true;
            inventoryManager.openInventory();
            dialogueManager.dialoguePromptWithSprite(sellWhat, shopKeeper);
        }
    }

    public void triggerBuyItem()
    {
        shopUI.SetActive(true);
        for (int i =0; i < itemsForSale.Length; i++)
        {
            Instantiate(itemsForSale[i], shopSlot[i].transform, false).transform.SetAsLastSibling();
        }
        isBuying = true;
        dialogueManager.dialoguePromptWithSprite(buyWhat, shopKeeper);
        waiting = true;
    }

    public void buyThis(GameObject buyingThis)
    {
        itemToBuy = buyingThis;
        dialogueManager.dialoguePromptWithSprite(howMany, shopKeeper);
        resetCounter();
        counter.SetActive(true);
        waiting = true;
        shopUI.SetActive(false);
        inventoryManager.isNotMousedOver();
    }

    public void buyThisMany()
    {
        switch (inventoryManager.purchaseItem(itemToBuy, buySellCount, itemToBuy.GetComponent<ItemProperties>().value))
        {
            case 0: //no prob
                dialogueManager.dialoguePromptWithSprite(buyingDialogue, shopKeeper);
                boughtSold = true;
                break;
            case -1://no space
                dialogueManager.dialoguePromptWithSprite(noSpaceDialogue, shopKeeper);
                break;
            case -2: //no money
                dialogueManager.dialoguePromptWithSprite(noMoneyDialogue, shopKeeper);
                break;
            default:
                Debug.Log("bloop");
                break;
        }
        waiting = false;
        pleaseEndConvo = true;

        isBuying = false;
    }



    public void outFromShop()
    {
        if(boughtSold)
        {
            dialogueManager.dialoguePromptWithSprite(goodbyeDialogue, shopKeeper);
        }
        resetShop();
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
                resetCounter();
                counter.SetActive(true);
            }
            else 
            {
                //sell the one
                sold(item, itemProperties.value, 1);
                dialogueManager.dialoguePromptWithSprite(hereIsYourChange, shopKeeper);
                pleaseEndConvo = true;
                waiting = false;
            }
        }
        else //item that has no value can't be sold
        {
            dialogueManager.dialoguePromptWithSprite(cantSell, shopKeeper);
            inventoryManager.closeInventory();
            pleaseEndConvo = true;
            waiting = false;
            isSelling = false;
        }
    }

    public void plusCounter()
    {
        if (isBuying)
        {
            buySellCount++;
            counterText.GetComponent<Text>().text = buySellCount.ToString();
        }
        else
        {
            buySellCount++;
            int count = inventoryManager.getCount(itemToSell);
            if (buySellCount > count)
            {
                buySellCount = count;
            }
            counterText.GetComponent<Text>().text = buySellCount.ToString();
        }
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
    

    public void ThisMany()
    {
        counter.SetActive(false);
        if (buySellCount <= 0)
        {
            dialogueManager.dialoguePromptWithSprite(changeYourMind, shopKeeper);
            pleaseEndConvo = true;
            waiting = false;
            isSelling = false;
            isBuying = false;
        }
        else if (isBuying)
        {
            buyThisMany();
        }
        else
        {
            sold(itemToSell, itemToSell.GetComponent<ItemProperties>().value, buySellCount);
            dialogueManager.dialoguePromptWithSprite(hereIsYourChange, shopKeeper);
            pleaseEndConvo = true;
            waiting = false;

        }
    }

    public void sold(GameObject item, int value, int count)
    {
        inventoryManager.addGold(value * count);
        inventoryManager.removeItem(item, count);
        inventoryManager.closeInventory();
        boughtSold = true;
        isSelling = false;
        
        
    }

    void resetCounter()
    {
        buySellCount = 0;
        counterText.GetComponent<Text>().text = 0.ToString();
    }

    public void closeShop()
    {
        waiting = false;
        dialogueManager.dialoguePromptWithSprite(goodbyeDialogue, shopKeeper);
        keyboardInputManager.enableCharacterMovement();
    }

    public bool isBuyingItem()
    {
        return isBuying;
    }
    public bool isSellingItem()
    {
        return isSelling;
    }
}
