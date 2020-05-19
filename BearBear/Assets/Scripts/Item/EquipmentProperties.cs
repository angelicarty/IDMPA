using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType {HEAD, NECK, HAND }
public class EquipmentProperties : MonoBehaviour
{
    //simply stores the stat buffs provided by the equipment, cleaner than putting it in item properties

    public EquipType type;
    public int ATK;
    public int DEF;
    public int SATK;
    public int SDEF;
    public int SPD;
}
