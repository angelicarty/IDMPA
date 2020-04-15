using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Heal : Item
{
    public int health;
    public new const ItemType type = ItemType.Heal;

    override public void Use(ref int targetCombatHP, Stats target)
    {
        if (!used)
        {
            if (targetCombatHP > 0)
            {//using in combat
                targetCombatHP += health;
            }
            else
            {
                target.SetCHP(target.GetCHP() + health);
            }
            used = true;
        } 
    }
}
