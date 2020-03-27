using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    float offsetX, offsetY;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startDrag();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y, 0);

    }

    void startDrag()
    {
        Debug.Log("start");
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;
    }

}
