﻿using System.Collections;
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
    public RectTransform p_display;
    public RectTransform e_display;
    //name displays
    public Text p_name;
    public Text e_name;

    public Image[] actionButtons;//goes clockwise from attack

    void OnEnable()
    {
        //Debug.Log("battle scene init thing loaded");
        Instantiate(controller.GetPlayer().battleActor, leftStage.transform);
        Instantiate(controller.GetEnemy().battleActor, rightStage.transform);
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
        p_display.localScale = new Vector3(calcBarLength(controller.GetPlayerHP(0), controller.GetPlayerHP(1)), 1, 1);
        e_display.localScale = new Vector3(calcBarLength(controller.GetEnemyHP(0), controller.GetEnemyHP(1)), 1, 1);
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

}
