﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This script controls the battle scene and damage calculations and stuff. Its attached object is created when the player collides with an enemy
public enum BattleState {START, ACTION, BAG, P1, P2, END}
public enum ActionType {ATTACK, ITEM, SPECIAL}


public class Battle : MonoBehaviour
{
    public int delay;//amount of time between stuff happening
    public BattleState state;
    private ActionType action;
    public GameObject over_player;//the continuous player object, not the battle scene one
    public GameObject over_enemy;//the enemy the player collided with to begin combat, determines combatants and their stats


    public GameObject battleScene;
    private BattleUI battleUI;
    public Camera overworld_camera;
    private Stats player;
    private Stats enemy;

    private int p_HP;
    private int e_HP;

    private int result = 0;//0 if combat is ongoing, 1 if player wins, -1 of losses

    public StatGen statGen;
    public InventoryManager inv;
    public EquipmentManager equipment;
    private ItemProperties used_item_prop;//when player uses an item, it gets stored here
    public bool debug = false;

    //stores stats after modification from equipment, etc. Index 0 is MHP, so is ignored
    private int[] effective_stats = new int[6];

    private System.Random ran = new System.Random();
    public void InitBattle()
    {        
        player = over_player.GetComponent<Stats>();
        enemy = over_enemy.GetComponent<Stats>();
        p_HP = player.GetCHP();
        e_HP = enemy.GetCHP();
        state = BattleState.START;
        overworld_camera.gameObject.SetActive(false);
        battleScene.SetActive(true);
        gameObject.GetComponent<UIManager>().MoveCanvas(battleScene.GetComponentInChildren<Canvas>());
        statGen.resetStatGen();
        battleUI = battleScene.GetComponent<BattleUI>();
        UpdateStats();
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
            else if (state == BattleState.ACTION || state == BattleState.BAG)
            {
                while (state == BattleState.ACTION || state == BattleState.BAG)
                {
                    if (Input.GetKeyDown("escape") || Input.GetKeyDown("i"))
                    {
                        inv.closeInventory();
                        state = BattleState.ACTION;
                    }
                    //waits for the player to push an action button, as defined in the ui stuff
                    yield return null;
                }

            }
            
            else if (state == BattleState.P1)//PLAYER GOES FIRST
            {
                switch (action)
                {
                    case ActionType.ATTACK:

                        e_HP = Attack(effective_stats[(int)Stat.ATK], e_HP, enemy.GetDEF());
                        battleUI.PlayerAttacking();
                        statGen.addProb((int)Stat.ATK);//ATTACK INCREASES WHEN PLAYER USES NORMAL ATTACK
                        if (debug) { Debug.Log("e_HP = " + e_HP); }
                        yield return new WaitForSeconds(delay);
                        if (e_HP <= 0)
                        {
                            battleUI.EnemyDeath();
                            result = 1;
                            state = BattleState.END;
                        }
                        else
                        {
                            p_HP = Attack(enemy.GetATK(), p_HP, effective_stats[(int)Stat.DEF]);
                            battleUI.PlayerDefending();

                            statGen.addProb((int)Stat.DEF);//DEFENCE INCREASES WHEN PLAYER IS HIT BY NORMAL ATTACK
                            if (debug) { Debug.Log("p_HP = " + p_HP); }
                            yield return new WaitForSeconds(delay);
                            if (p_HP <= 0)
                            {
                                battleUI.PlayerDeath();
                                result = -1;
                                state = BattleState.END;
                            }
                            else
                            {
                                state = BattleState.ACTION;
                            }
                        }
                        break;

                    case ActionType.ITEM:
                        //use item
                        if (used_item_prop.isEdible)
                        {
                            Heal(used_item_prop.Eat());
                            yield return new WaitForSeconds(delay);
                        }
                        else if (used_item_prop.isEquipment)
                        {
                            UpdateStats();
                            yield return new WaitForSeconds(delay);
                        }
                        //enemy attack
                        p_HP = Attack(enemy.GetATK(), p_HP, effective_stats[(int)Stat.DEF]);
                        battleUI.PlayerDefending();
                        statGen.addProb((int)Stat.DEF);//DEFENCE INCREASES WHEN PLAYER IS HIT BY NORMAL ATTACK
                        if (debug) { Debug.Log("p_HP = " + p_HP); }
                        yield return new WaitForSeconds(delay);
                        if (p_HP <= 0)
                        {
                            battleUI.PlayerDeath();
                            result = -1;
                            state = BattleState.END;
                        }
                        else
                        {
                            state = BattleState.ACTION;
                        }
                        break;

                    case ActionType.SPECIAL:
                        //TODO: special stuff
                        break;
                }//switch end

            }
            else if (state == BattleState.P2)//PLAYER GOES SECOND
            {
                switch (action)
                {
                    case ActionType.ATTACK:
                        //enemy attack
                        p_HP = Attack(enemy.GetATK(), p_HP, effective_stats[(int)Stat.DEF]);
                        battleUI.PlayerDefending();
                        statGen.addProb((int)Stat.DEF);//DEFENCE INCREASES WHEN PLAYER IS HIT BY NORMAL ATTACK
                        if (debug) { Debug.Log("p_HP = " + p_HP); }
                        yield return new WaitForSeconds(delay);
                        if (p_HP <= 0)
                        {
                            battleUI.PlayerDeath();
                            result = -1;
                            state = BattleState.END;
                        }
                        else
                        {
                            e_HP = Attack(effective_stats[(int)Stat.ATK], e_HP, enemy.GetDEF());
                            battleUI.PlayerAttacking();
                            statGen.addProb((int)Stat.ATK);//ATTACK INCREASES WHEN PLAYER USES NORMAL ATTACK
                            if (debug) { Debug.Log("e_HP = " + e_HP); }
                            yield return new WaitForSeconds(delay);
                            if (e_HP <= 0)
                            {
                                battleUI.EnemyDeath();
                                result = 1;
                                state = BattleState.END;
                            }
                            else
                            {
                                state = BattleState.ACTION;
                            }
                        }
                        break;

                    case ActionType.ITEM:
                        //enemy attack
                        p_HP = Attack(enemy.GetATK(), p_HP, effective_stats[(int)Stat.DEF]);
                        statGen.addProb((int)Stat.DEF);//DEFENCE INCREASES WHEN PLAYER IS HIT BY NORMAL ATTACK
                        battleUI.PlayerDefending();
                        if (debug) { Debug.Log("p_HP = " + p_HP); }
                        yield return new WaitForSeconds(delay);
                        if (p_HP <= 0)
                        {
                            battleUI.PlayerDeath();
                            result = -1;
                            state = BattleState.END;
                        }
                        else
                        {
                            state = BattleState.ACTION;
                        }
                        //use item
                        if (used_item_prop.isEdible)//heal
                        {
                            Heal(used_item_prop.Eat());
                            yield return new WaitForSeconds(delay);
                        }
                        else if (used_item_prop.isEquipment)//equip
                        {
                            UpdateStats();
                            yield return new WaitForSeconds(delay);
                        }
                        break;

                    case ActionType.SPECIAL:
                        //TODO: special stuff
                        break;

                }//switch end
            }
        }
        yield return new WaitForSeconds(delay * 2);
        //do shit based on result of battle
        GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<Animator>().SetTrigger("Conceal");
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<UIManager>().MoveCanvas(GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>());
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


    private int Attack(int o_damage, int d_health, int d_defense)
    {//calculates the damage of an attack, outputs resulting health
        //TODO: an actual formula, defence, lots
        float moddifier = Random.Range(9, 12) / 10.0f;
        if (debug) { Debug.Log("MOD: " + moddifier); }
        float damage = (o_damage / d_defense) * moddifier * 1.0f;
        if (debug) { Debug.Log("DAMAGE: " + damage + " RESULTING HEALTH: " + (d_health - damage)); }
        return Mathf.RoundToInt(d_health - damage);
    }

    private void Heal(int health)
    {

        Debug.Log("healing: " + p_HP + " goes to " + (p_HP + health));
        p_HP += health;
        if (p_HP > player.GetMHP())
        {
            p_HP = player.GetMHP();
        }
        battleUI.PlayerHeal();
    }

    //PLAYER ACTION CHOICES
    public void ActionAttack()
    {
        action = ActionType.ATTACK;
        if (effective_stats[(int)Stat.SPD] >= enemy.GetSPD())
        {
            state = BattleState.P1;
        }
        else
        {
            state = BattleState.P2;
            statGen.addProb((int)Stat.SPD);//SPEED INCREASES IF PLAYER GOES SECOND TODO
        }
    }

    public void ActionRun()
    {
        result = 2;
        state = BattleState.END;
    }

    public void ActionItem()
    {
        used_item_prop = null;
        inv.openInventory();
        state = BattleState.BAG;



    }

    public void ActionSelectItem(ItemProperties item)
    {
        if (state == BattleState.BAG)
        {
            inv.closeInventory();
            action = ActionType.ITEM;
            used_item_prop = new ItemProperties(item);
            if (effective_stats[(int)Stat.SPD] >= enemy.GetSPD())
            {
                state = BattleState.P1;
            }
            else
            {
                state = BattleState.P2;
                statGen.addProb((int)Stat.SPD);//SPEED INCREASES IF PLAYER GOES SECOND TODO
            }
        }
    }

    //UPDATE CURRENT STATS
    private void UpdateStats()
    {
        EquipmentProperties hand = equipment.GetHand();
        if (hand == null)
            hand = new EquipmentProperties();
        EquipmentProperties head = equipment.GetHead();
        if (head == null)
            head = new EquipmentProperties();
        EquipmentProperties neck = equipment.GetNeck();
        if (neck == null)
            neck = new EquipmentProperties();

        effective_stats[(int)Stat.ATK] = player.GetATK() + hand.ATK + head.ATK + neck.ATK;
        effective_stats[(int)Stat.DEF] = player.GetDEF() + hand.DEF + head.DEF + neck.DEF;
        effective_stats[(int)Stat.SATK] = player.GetSATK() + hand.SATK + head.SATK + neck.SATK;
        effective_stats[(int)Stat.SDEF] = player.GetSDEF() + hand.SDEF + head.SDEF + neck.SDEF;
        effective_stats[(int)Stat.SPD] = player.GetSPD() + hand.SPD + head.SPD + neck.SPD;
    }

    //ENDING STUFF
    private void EndWin()
    {
        RunStatGen(1);
        Debug.Log("You win!");

        Loot();
        FindObjectOfType<QuestManager>().Killed(over_enemy.name);

        battleScene.SetActive(false);
        overworld_camera.gameObject.SetActive(true);
        FindObjectOfType<KeyboardInputManager>().enableChat(); //resume pressing space to  chat 

        GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<Animator>().SetTrigger("Reveal");
        Destroy(over_enemy);
    }

    private void EndLose()
    {
        RunStatGen(0.5f);
        Debug.Log("You lose!");
        //TODO: player death stuff
        FindObjectOfType<KeyboardInputManager>().enableChat(); //resume pressing space to  chat 
        FindObjectOfType<KeyboardInputManager>().enableCharacterMovement(); //re-enable character movement
        FindObjectOfType<MonstersController>().goingIntoMobArea(); //resume monster movements
        FindObjectOfType<PlayerManager>().Death();
        battleScene.SetActive(false);
        overworld_camera.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<Animator>().SetTrigger("Reveal");
    }

    private void EndRun()
    {
        Debug.Log("You run away!");
        battleScene.SetActive(false);
        overworld_camera.gameObject.SetActive(true);
        FindObjectOfType<KeyboardInputManager>().enableChat(); //resume pressing space to  chat 
        FindObjectOfType<KeyboardInputManager>().enableCharacterMovement(); //re-enable character movement
        FindObjectOfType<MonstersController>().goingIntoMobArea(); //resume monster movements

        GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<Animator>().SetTrigger("Reveal");
    }

    //AWARDING LOOT
    private void Loot()
    {
        if (over_enemy.GetComponent<MobDropContainer>().drop == null)
        {
            FindObjectOfType<KeyboardInputManager>().enableCharacterMovement(); //re-enable character movement
            FindObjectOfType<MonstersController>().goingIntoMobArea(); //resume monster movements
        }
        Debug.Log("Awarding loot");
        inv.giveItem(over_enemy.GetComponent<MobDropContainer>().drop, 1);

    }

    //STAT GEN STUFF
    private void RunStatGen(float mod)
    {
        bool[] table = statGen.calcStat();

        if(table[(int)Stat.MHP])
            player.ModMHP(statGen.value_scale);
        if (table[(int)Stat.ATK])
            player.ModATK(Mathf.FloorToInt(statGen.value_scale * mod));
        if (table[(int)Stat.DEF])
            player.ModDEF(Mathf.FloorToInt(statGen.value_scale * mod));
        if (table[(int)Stat.SATK])
            player.ModSATK(Mathf.FloorToInt(statGen.value_scale * mod));
        if (table[(int)Stat.SDEF])
            player.ModSDEF(Mathf.FloorToInt(statGen.value_scale * mod));
        if (table[(int)Stat.SPD])
            player.ModSPD(Mathf.FloorToInt(statGen.value_scale * mod));
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
