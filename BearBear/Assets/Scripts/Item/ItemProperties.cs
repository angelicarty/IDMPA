using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class ItemProperties : MonoBehaviour
{
    public string name;
    public int health;
    public bool used = false;//temporary flag tripped when item is used to prevent reuse before it can be removed from the inventory, BREAKS IT TODO FIX
    public bool isEdible;
    public bool isEquipment;
    public int value;//sale value of the item
    [TextArea (3,10)]
    public string itemDescription;
    public int Eat()
    {
        return health;
    }

}
