using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public GameObject equipmentUI;
    public GameObject slot_weapon;
    public GameObject slot_hat;
    public GameObject slot_neck;

    public void show()
    {
        equipmentUI.SetActive(true);

    }

    public void hide()
    {
        equipmentUI.SetActive(false);
    }



    //hover description box stuff
    public GameObject itemDescBox;
    public GameObject itemDescText;
    public GameObject itemNameText;

    float offsetX, offsetY;

    bool isMousedOverItem;

    public void isMousedOver(string thisName, string desc)
    {
        isMousedOverItem = true;
        var childCount = itemDescBox.transform.parent.childCount;
        itemDescBox.transform.SetAsLastSibling();
        offsetX = -(itemDescBox.GetComponent<RectTransform>().sizeDelta.x + 10) / 2;
        offsetY = -(itemDescBox.GetComponent<RectTransform>().sizeDelta.y + 10) / 2;
        itemDescText.GetComponent<Text>().text = desc;
        itemNameText.GetComponent<Text>().text = thisName;
        mouseOverItem();
    }

    void mouseOverItem()
    {
        itemDescBox.SetActive(true);
        itemDescBox.transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y, 0);
    }

    public void isNotMousedOver()
    {
        isMousedOverItem = false;
        itemDescBox.SetActive(false);
    }
}
