using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class implemented by specific item types e.g. healing, stat buffing, damaging, etc.

public enum ItemType { Null, Heal, Damage }
public abstract class Item : MonoBehaviour
{
    public const ItemType type = ItemType.Null;
    public bool used = false;//temporary flag tripped when item is used to prevent reuse before it can be removed from the inventory

    public abstract void Use(ref int targetCombatHP, Stats target);
}
