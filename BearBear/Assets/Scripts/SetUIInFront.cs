using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetUIInFront : MonoBehaviour, IPointerDownHandler
{

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        var childCount = transform.parent.childCount;
        transform.SetSiblingIndex(childCount - 2);
    }

}

