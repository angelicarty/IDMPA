using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetUIInFront : MonoBehaviour, IPointerDownHandler
{

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        //var childCount = transform.parent.childCount;
        //transform.SetSiblingIndex(childCount - 2);
    }

    public void OnEnable()
    {
        transform.SetAsLastSibling();
    }

}

