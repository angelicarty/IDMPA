using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//controls the ui for the battle scene
public class BattleUI : MonoBehaviour
{
    public Battle controller;
    public GameObject actions;
    //draws the appropriate sprite for those involved in combat
    public GameObject leftStage;
    public GameObject rightStage;
    //health bar display stuff
    public UpdateHealth p_display;
    public UpdateHealth e_display;
    //name displays
    public Text p_name;
    public Text e_name;

    public Image[] actionButtons;//goes clockwise from attack

    private GameObject p_actor;
    private GameObject e_actor;
    private Animator p_anim;
    private Animator e_anim;

    void OnEnable()
    {
        UpdateHealthBar(true);
        UpdateHealthBar(false);
        //Debug.Log("battle scene init thing loaded");
        p_actor = Instantiate(controller.GetPlayer().battleActor, leftStage.transform);
        e_actor = Instantiate(controller.GetEnemy().battleActor, rightStage.transform);
        p_anim = p_actor.GetComponent<Animator>();
        e_anim = e_actor.GetComponent<Animator>();
        p_name.text = controller.GetPlayer().name;
        e_name.text = controller.GetEnemy().name;


    }

    void OnDisable()
    {
        //Debug.Log("removing actors");
        Destroy(leftStage.transform.GetChild(0).gameObject);
        Destroy(rightStage.transform.GetChild(0).gameObject);

    }

    void Update()
    {
        if (controller.GetState() == BattleState.ACTION)
        {

            actions.SetActive(true);
        }
        else
        {
            actions.SetActive(false);
        }


    }

    public void highlightButton(int index)
    {
        for (int i = 0; i < actionButtons.Length; i++)
        {
            actionButtons[i].color = Color.white;
        }
        if (index > -1)
        {
            actionButtons[index].color = Color.blue;
        }
    }

    private float calcBarLength(int max, int current)
    { 
        float value = (float)current/max;
        if (value < 0)
        {
            return 0;
        }
        if (value > 1)
        {
            return 1;
        }
        return value;
    }

    //TODO: set up the timing for these
    public void PlayerAttacking()
    {
        p_anim.SetTrigger("Attacking");
        e_anim.SetTrigger("Defending");
        UpdateHealthBar(false);
    }

    public void PlayerDefending()
    {
        p_anim.SetTrigger("Defending");
        e_anim.SetTrigger("Attacking");
        UpdateHealthBar(false);
    }

    public void PlayerDeath()
    {
        p_anim.SetBool("Dead", true);
    }
    public void EnemyDeath()
    {
        e_anim.SetBool("Dead", true);
    }

    public void PlayerHeal()
    {
        UpdateHealthBar(false);
    }
    private void UpdateHealthBar(bool reset)
    {
        if (reset)
        {
            p_display.reset = true;
            e_display.reset = true;
        }
        p_display.scale = 1 - calcBarLength(controller.GetPlayerHP(0), controller.GetPlayerHP(1));
        e_display.scale = 1 - calcBarLength(controller.GetEnemyHP(0), controller.GetEnemyHP(1));

        p_display.StartCoroutine("UpdateHealthBar");
        e_display.StartCoroutine("UpdateHealthBar");
    }


}
