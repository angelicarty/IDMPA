using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    float offsetX, offsetY;
    public GameObject thingToMove;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startDrag();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        thingToMove.transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y, 0);

    }

    void startDrag()
    {
        offsetX = thingToMove.transform.position.x - Input.mousePosition.x;
        offsetY = thingToMove.transform.position.y - Input.mousePosition.y;
    }

}
