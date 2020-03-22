using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //TODO: initialisation, levelup
    [SerializeField]
    private int MHP = 0;//max health    
    [SerializeField]
    private int CHP = 0;//current health
    [SerializeField]
    private int ATK = 0;
    [SerializeField]
    private int SPD = 0;

    //sprite used for battle, includes animations eventually
    public GameObject battleActor;
    public string name;

    //GET
    public int GetMHP()
    {
        return MHP;
    }

    public int GetCHP()
    {
        return CHP;
    }

    public int GetATK()
    {
        return ATK;
    }

    public int GetSPD()
    {
        return SPD;
    }

    //SET
    public bool SetCHP(int newVal)
    {
        //hp can never exceed max
        if (newVal > MHP)
        {
            return false;
        }
        CHP = newVal;
        return true;
    }

    //MODIFY, used by file i/o and "level ups" from creature death
    public void ModMHP(int i)
    {
        MHP += i;
    }

    public void ModATK(int i)
    {
        ATK += i;
    }

    public void ModSPD(int i)
    {
        SPD += i;
    }

}
