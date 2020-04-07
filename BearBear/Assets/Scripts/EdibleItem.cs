using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EdibleItem : MonoBehaviour
{
    public Stats statToRecover;
    public int HPToRecover, MPToRecover;
    int currentHP, currentMP;
    public string ItemName;
    public Texture2D icon;

    public void eatItem()
    {
        currentHP = statToRecover.GetCHP();
        currentHP += HPToRecover;
        statToRecover.SetCHP(currentHP);

        //to be implemented after we get skills working
        //currentMP = statToRecover.GetMMP();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            
        }

    }
}
