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
        FindObjectOfType<InventoryManager>().isNotMousedOver();
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

    //gets all equipment IDs for saving
    public string[] GetAllIds()
    {
        string[] IDs = new string[3];
        try
        {
             IDs[0] = slot_hand.GetComponentInChildren<EquipmentProperties>().name.Replace("(Clone)", "");
        }
        catch (System.Exception e)
        {
            IDs[0] = "-";
        }
        try
        {
            IDs[1] = slot_head.GetComponentInChildren<EquipmentProperties>().name.Replace("(Clone)", "");
        }
        catch (System.Exception e)
        {
            IDs[1] = "-";
        }
        try
        {
            IDs[2] = slot_neck.GetComponentInChildren<EquipmentProperties>().name.Replace("(Clone)", "");
        }
        catch (System.Exception e)
        {
            IDs[2] = "-";
        }
        return IDs;
    }
}
