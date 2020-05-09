using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfClick : MonoBehaviour
{
    public void Clicked()
    {
        var shop = FindObjectOfType<ShopManager>();
        bool inShop = shop.isPlayerInShop();
        if (inShop)
        {
            shop.sellThis(gameObject);
        }
        else
        {
            try
            {
                ItemProperties food = gameObject.GetComponent<ItemProperties>();//checks if the item is edible
                FindObjectOfType<InventoryManager>().EatItem(gameObject);
            }
            catch (Exception e)
            {
                Debug.LogError("item not edible");
            }
        }
    }
}
