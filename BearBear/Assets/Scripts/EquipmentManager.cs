using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public GameObject equipmentUI;
    public GameObject slot_hand;
    public GameObject slot_head;
    public GameObject slot_neck;

    public void show()
    {
        equipmentUI.SetActive(true);

    }

    public void hide()
    {
        equipmentUI.SetActive(false);
    }

    public EquipmentProperties GetHand()
    {
        try
        {
            return slot_hand.GetComponentInChildren<EquipmentProperties>();
        }
        catch (System.Exception e)
        {
            return null;
        }
    }

    public EquipmentProperties GetHead()
    {
        try
        {
            return slot_head.GetComponentInChildren<EquipmentProperties>();
        }
        catch (System.Exception e)
        {
            return null;
        }
    }

    public EquipmentProperties GetNeck()
    {
        try
        {
            return slot_neck.GetComponentInChildren<EquipmentProperties>();
        }
        catch (System.Exception e)
        {
            return null;
        }
    }
}
