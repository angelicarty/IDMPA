using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Edible : MonoBehaviour
{
    public int health;
    public bool used = false;//temporary flag tripped when item is used to prevent reuse before it can be removed from the inventory, BREAKS IT TODO FIX

    public int Use()
    {
        return health;
    }
}
