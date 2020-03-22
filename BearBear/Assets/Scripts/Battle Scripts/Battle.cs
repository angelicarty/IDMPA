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
    public Camera overworld_camera;
    private Stats player;
    private Stats enemy;

    private int p_HP;
    private int e_HP;

    private int result = 0;//0 if combat is ongoing, 1 if player wins, -1 of losses

    public bool debug = false;
    public void InitBattle()
    {
        FindObjectOfType<KeyboardInputManager>().disableCharacterMovement(); //disable character movement
        FindObjectOfType<KeyboardInputManager>().disableChat(); //prevents opening chat while in battle
        FindObjectOfType<MonstersController>().goingOutOfMobArea(); //pauses monster movements
        player = over_player.GetComponent<Stats>();
        enemy = over_enemy.GetComponent<Stats>();
        p_HP = player.GetCHP();
        e_HP = enemy.GetCHP();
        state = BattleState.START;
        overworld_camera.gameObject.SetActive(false);
        battleScene.SetActive(true);
        StartCoroutine("BattleLoop");
    }

    IEnumerator BattleLoop()
    {

        if (debug) { Debug.Log("BATTLELOOP START"); }
        //TODO: clean this garbage
        while (state != BattleState.END)
        {
            if (debug) { Debug.Log("WHILELOOP BEGINNING"); }
            if (state == BattleState.START)
            {
                state = BattleState.ACTION;
            }
            else if (state == BattleState.ACTION)
            {
                while (state == BattleState.ACTION)
                {
                    //waits for the player to push an action button, as defined in the ui stuff
                    yield return null;
                }

            }
            
            else if (state == BattleState.P1)
            {
                //TODO: real damage calculations
                e_HP = Attack(player.GetATK(), e_HP);
                if (debug) { Debug.Log("e_HP = " + e_HP); }
                yield return new WaitForSeconds(delay);
                if (e_HP <= 0)
                {
                    result = 1;
                    state = BattleState.END;
                }
                else 
                {
                    p_HP = Attack(enemy.GetATK(), p_HP);
                    if (debug) { Debug.Log("p_HP = " + p_HP); }
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
                if (debug) { Debug.Log("p_HP = " + p_HP); }
                yield return new WaitForSeconds(delay);
                if (p_HP <= 0)
                {
                    result = -1;
                    state = BattleState.END;
                }
                else
                {
                    e_HP = Attack(player.GetATK(), e_HP);
                    if (debug) { Debug.Log("e_HP = " + e_HP); }
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
        player.SetCHP(p_HP);
        enemy.SetCHP(e_HP);
        if (result == 1)
        {
            EndWin();
        }
        else if (result == 2)
        {
            EndRun();
        }
        else
        {
            EndLose();
        }
        if (debug) { Debug.Log("BATTLELOOP END"); }
    }


    private int Attack(int offensive_damage, int defensive_health)
    {//calculates the damage of an attack, outputs resulting health
        //TODO: an actual formula, defence, lots
        return defensive_health - offensive_damage;
    }

    //PLAYER ACTION CHOICES
    public void ActionAttack()
    {
        if (player.GetSPD() >= enemy.GetSPD())
        {
            state = BattleState.P1;
        }
        else
        {
            state = BattleState.P2;
        }
    }

    public void ActionRun()
    {
        result = 2;
        state = BattleState.END;
    }


    //ENDING STUFF
    private void EndWin()
    {
        Debug.Log("You win!");
        //TODO: award xp/loot/whatever
        FindObjectOfType<QuestManager>().killed(over_enemy.name);
        Destroy(over_enemy);
        battleScene.SetActive(false);
        overworld_camera.gameObject.SetActive(true);
        FindObjectOfType<KeyboardInputManager>().enableCharacterMovement(); //re-enable character movement
        FindObjectOfType<KeyboardInputManager>().enableChat(); //resume pressing space to  chat 
        FindObjectOfType<MonstersController>().goingIntoMobArea(); //resume monster movements
    }

    private void EndLose()
    {
        Debug.Log("You lose!");
        SceneManager.LoadScene("TitleMenu");//kicks the player to the title screen, easiest game over thing, though probably shouldn't be done here
    }

    private void EndRun()
    {
        Debug.Log("You run away!");
        battleScene.SetActive(false);
        overworld_camera.gameObject.SetActive(true);
        FindObjectOfType<KeyboardInputManager>().enableCharacterMovement(); //re-enable character movement
        FindObjectOfType<KeyboardInputManager>().enableChat(); //resume pressing space to  chat 
        FindObjectOfType<MonstersController>().goingIntoMobArea(); //resume monster movements
    }




    //GETTERS AND SETTERS
    public BattleState GetState()
    {
        return state;
    }

    public Stats GetPlayer()
    {
        return player;
    }

    public Stats GetEnemy()
    {
        return enemy;
    }

    public int GetPlayerHP(int i)
    {//input 0 to get maxph
        if (i > 0)
        {
            return p_HP;
        }
        else 
        {
            return player.GetMHP();
        }
    }

    public int GetEnemyHP(int i)
    {//input 0 to get maxph
        if (i > 0)
        {
            return e_HP;
        }
        else
        {
            return enemy.GetMHP();
        }
    }
}
