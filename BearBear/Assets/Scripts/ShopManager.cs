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
    bool inShop = false;

    public void triggerShop(Dialogue welcomeDia,Dialogue noMuns,Dialogue noSpace,Dialogue bye ,GameObject[] items)
    {
        welcomeDialogue = welcomeDia;
        itemsForSale = items;
        noMoneyDialogue = noMuns;
        noSpaceDialogue = noSpace;
        goodbyeDialogue = bye;
        inShop = true;
    }

    public void resetShop()
    {
        welcomeDialogue = null;
        itemsForSale = null;
        noMoneyDialogue = null;
        noSpaceDialogue = null;
        goodbyeDialogue = null;
        inShop = false;
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
