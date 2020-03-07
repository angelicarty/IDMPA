﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //TODO: initialisation, levelup
    [SerializeField]
    private int MHP;//max health    
    [SerializeField]
    private int CHP;//current health
    [SerializeField]
    private int ATK;
    [SerializeField]
    private int SPD;

    //sprite used for battle
    public Sprite battleSprite;

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

}