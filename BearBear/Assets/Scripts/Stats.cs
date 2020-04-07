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
    /* to be implemented AFTER we get skills working
    [SerializeField]
    private int MMP = 0; //max MP
    [SerializeField]
    private int CMP = 0;//current MP
    */
    [SerializeField]
    private int ATK = 0;
    [SerializeField]
    private int DEF = 0;
    [SerializeField]
    private int sATK = 0;
    [SerializeField]
    private int sDEF = 0;
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
    public int GetDEF()
    {
        return DEF;
    }
    public int GetSATK()
    {
        return sATK;
    }
    public int GetSDEF()
    {
        return sDEF;
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
    public void ModDEF(int i)
    {
        DEF += i;
    }
    public void ModSATK(int i)
    {
        sATK += i;
    }
    public void ModSDEF(int i)
    {
        sDEF += i;
    }
    public void ModSPD(int i)
    {
        SPD += i;
    }

}
