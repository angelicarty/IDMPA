using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public EdibleItem item1;
    public GameObject[] inventorySlots;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("k"))
        {
            item1.eatItem();
        }


    }

    public void getItem(EdibleItem item)
    {
        
    }
}
