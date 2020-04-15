using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Damage : Item
{
    public int health;
    public new const ItemType type = ItemType.Damage;

    override public void Use(ref int targetCombatHP, Stats target)
    {
        if (!used)
        {
            if (targetCombatHP > 0)
            {//using in combat
                targetCombatHP -= health;
                used = true;
            }
            //assumed it can't be used outside of combat
        }
    }
}
