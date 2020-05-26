using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOverItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<InventoryManager>().isMousedOver(gameObject.GetComponent<ItemProperties>().name, gameObject.GetComponent<ItemProperties>().itemDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<InventoryManager>().isNotMousedOver();
    }

}
