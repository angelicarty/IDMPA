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
        else if (gameObject.GetComponentInParent<EquipSlotFlag>() != null)
        {
            FindObjectOfType<InventoryManager>().UnequipItem(gameObject);
        }
        else
        {
            FindObjectOfType<InventoryManager>().UseItem(gameObject);
        }
    }
}
