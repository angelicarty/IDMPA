using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script controls the battle scene and damage calculations and stuff. Its attached object is created when the player collides with an enemy
public enum BattleState {START, ACTION, P1, P2, END}
public class Battle : MonoBehaviour
{
    public BattleState state;
    public GameObject over_player;//the continuous player object, not the battle scene one
    public GameObject over_enemy;//the enemy the player collided with to begin combat, determines combatants and their stats


    private GameObject bp;
    private Stats player;
    private Stats enemy;

    private int p_HP;
    private int e_HP;

    private int result = 0;//0 if combat is ongoing, 1 if player wins, -1 of losses
    public void Start()
    {

        bp = GameObject.FindGameObjectWithTag("BattleScene");
        player = over_player.GetComponent<Stats>();
        enemy = over_enemy.GetComponent<Stats>();
        p_HP = player.GetCHP();
        e_HP = enemy.GetCHP();
        state = BattleState.START;
        StartCoroutine("BattleLoop");
    }

    IEnumerator BattleLoop()
    {
        //TODO: clean this garbage
        while (state != BattleState.END)
        {
            if (state == BattleState.START)
            {
                state = BattleState.ACTION;
            }
            else if (state == BattleState.ACTION)
            {
                while ((!Input.GetButtonDown("Fire1")))
                {
                    yield return null;
                }
                if (player.GetSPD() >= enemy.GetSPD())
                {
                    state = BattleState.P1;
                }
                else
                {
                    state = BattleState.P2;
                }
            }
            
            else if (state == BattleState.P1)
            {
                //TODO: real damage calculations
                e_HP -= player.GetATK();
                if (e_HP <= 0)
                {
                    result = 1;
                    state = BattleState.END;
                }
                else 
                {
                    p_HP -= enemy.GetATK();
                    if (p_HP <= 0)
                    {
                        result = -1;
                        state = BattleState.END;
                    }
                    else
                    {
                        state = BattleState.ACTION;
                    }
                }
            }
            else if (state == BattleState.P2)
            {
                //TODO: real damage calculations
                p_HP -= player.GetATK();
                if (p_HP <= 0)
                {
                    result = -1;
                    state = BattleState.END;
                }
                else
                {
                    e_HP -= enemy.GetATK();
                    if (e_HP <= 0)
                    {
                        result = 1;
                        state = BattleState.END;
                    }
                    else 
                    {
                        state = BattleState.ACTION;
                    }
                }
            }
        }
    }

    private bool CombatLoop()
    {//main combat loop, returns true if player wins, false otherwise
        bool run = false;
        while (!run)
        {
            //get player action
            //TODO: determine oponent action

            //exectute in speed order
            if (player.GetSPD() >= enemy.GetSPD())
            {

            }
            else 
            {

            }

            //check for death
            if (p_HP <= 0)
            {
                return false;
            }


            if (player.GetSPD() >= enemy.GetSPD())
            {
                //TODO: real damage calculations
                p_HP -= enemy.GetATK();

            }
            else
            {
                e_HP -= player.GetATK();
            }

            //check for death
            if (p_HP <= 0)
            {
                return false;
            }
            else if (e_HP <= 0)
            {
                return true;
            }
        }

        return true;
    }

    public BattleState GetState()
    {
        return state;
    }

    public Sprite GetPlayerSprite()
    { 
        return player.GetComponent<SpriteRenderer>().sprite;
    }

    public Sprite GetEnemySprite()
    { 
        return enemy.GetComponent<SpriteRenderer>().sprite;
    }

    public int GetPlayerHP()
    {
        return p_HP;
    }

    public int GetEnemyHP()
    {
        return e_HP;
    }
}
