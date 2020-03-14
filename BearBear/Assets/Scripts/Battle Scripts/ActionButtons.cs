using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtons : MonoBehaviour
{
    public Battle controller;

    public void Attack() 
    {
        if (controller.GetState().Equals(BattleState.ACTION))
        {
            controller.ActionAttack();
        }
    }

    public void Run()
    {
        if (controller.GetState().Equals(BattleState.ACTION))
        {
            controller.ActionRun();
        }
    }

    public void Special()
    { 
    //todo
    }

    public void Item()
    { 
    //todo
    }
}
