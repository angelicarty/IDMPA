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
}
