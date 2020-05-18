using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfClick : MonoBehaviour
{
    public void Clicked()
    {
        var shop = FindObjectOfType<ShopManager>();
        if (shop.isBuyingItem())
        {
            shop.buyThis(gameObject);
        }
        else if (shop.isSellingItem())
        {
            shop.sellThis(gameObject);
        }
        else
        {
            try
            {
                ItemProperties food = gameObject.GetComponent<ItemProperties>();//checks if the item is edible
                FindObjectOfType<InventoryManager>().UseItem(gameObject);
            }
            catch (Exception e)
            {
                Debug.LogError("item not edible");
            }
        }
    }
}
