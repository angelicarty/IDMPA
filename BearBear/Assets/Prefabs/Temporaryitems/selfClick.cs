using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfClick : MonoBehaviour
{
    public void clickedApple()
    {
        FindObjectOfType<InventoryManager>().eatApple();
    }

    public void clickedBigApple()
    {
        FindObjectOfType<InventoryManager>().eatBigApple();
    }
}
