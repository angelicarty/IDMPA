using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This script controls the battle scene and damage calculations and stuff. Its attached object is created when the player collides with an enemy
public enum BattleState {START, ACTION, P1, P2, END}
public class Battle : MonoBehaviour
{
    public int delay;//amount of time between stuff happening
    public BattleState state;
    public GameObject over_player;//the continuous player object, not the battle scene one
    public GameObject over_enemy;//the enemy the player collided with to begin combat, determines combatants and their stats


    public GameObject battleScene;
    public GameObject actorParent;
    private Stats player;
    private Stats enemy;

    private int p_HP;
    private int e_HP;

    private int result = 0;//0 if combat is ongoing, 1 if player wins, -1 of losses
    public void Start()
    {
        player = over_player.GetComponent<Stats>();
        enemy = over_enemy.GetComponent<Stats>();
        p_HP = player.GetCHP();
        e_HP = enemy.GetCHP();
        state = BattleState.START;
        battleScene.SetActive(true);
        actorParent.SetActive(false);//prevents stuff from walking around during the battle
        StartCoroutine("BattleLoop");
    }

    IEnumerator BattleLoop()
    {

        Debug.Log("BATTLELOOP START");
        //TODO: clean this garbage
        while (state != BattleState.END)
        {
            Debug.Log("WHILELOOP BEGINNING");
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
                e_HP = Attack(player.GetATK(), e_HP);
                Debug.Log("e_HP = " + e_HP);
                yield return new WaitForSeconds(delay);
                if (e_HP <= 0)
                {
                    result = 1;
                    state = BattleState.END;
                }
                else 
                {
                    p_HP = Attack(enemy.GetATK(), p_HP);
                    Debug.Log("p_HP = " + p_HP);
                    yield return new WaitForSeconds(delay);
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
                p_HP = Attack(enemy.GetATK(), p_HP);
                Debug.Log("p_HP = " + p_HP);
                yield return new WaitForSeconds(delay);
                if (p_HP <= 0)
                {
                    result = -1;
                    state = BattleState.END;
                }
                else
                {
                    e_HP = Attack(player.GetATK(), e_HP);
                    Debug.Log("e_HP = " + e_HP);
                    yield return new WaitForSeconds(delay);
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
        //do shit based on result of battle
        if (result == 1)
        {
            Debug.Log("You win!");
            //TODO: award xp/loot/whatever
            Destroy(over_enemy);
            actorParent.SetActive(true);
            battleScene.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("You lose!");
            SceneManager.LoadScene("TitleMenu");//kicks the player to the title screen, easiest game over thing, though probably shouldn't be done here
        }
        Debug.Log("BATTLELOOP END");
    }

    //private bool CombatLoop()
    //{//OLD PLEASE IGNORE
    //    bool run = false;
    //    while (!run)
    //    {
    //        //get player action
    //        //TODO: determine oponent action

    //        //exectute in speed order
    //        if (player.GetSPD() >= enemy.GetSPD())
    //        {

    //        }
    //        else 
    //        {

    //        }

    //        //check for death
    //        if (p_HP <= 0)
    //        {
    //            return false;
    //        }


    //        if (player.GetSPD() >= enemy.GetSPD())
    //        {
    //            //TODO: real damage calculations
    //            p_HP -= enemy.GetATK();

    //        }
    //        else
    //        {
    //            e_HP = Attack(player.GetATK(), e_HP);
    //        }

    //        //check for death
    //        if (p_HP <= 0)
    //        {
    //            return false;
    //        }
    //        else if (e_HP <= 0)
    //        {
    //            return true;
    //        }
    //    }

    //    return true;
    //}

    private int Attack(int offensive_damage, int defensive_health)
    {//calculates the damage of an attack, outputs resulting health
        //TODO: an actual formula, defence, lots
        return defensive_health - offensive_damage;
    }

    public BattleState GetState()
    {
        return state;
    }

    public GameObject GetPlayerBA()
    { 
        return player.battleActor;
    }

    public GameObject GetEnemyBA()
    {
        return enemy.battleActor;
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
