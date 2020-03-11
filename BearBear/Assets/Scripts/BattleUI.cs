using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//controls the ui for the battle scene
public class BattleUI : MonoBehaviour
{
    public Battle controller;

    //draws the appropriate sprite for those involved in combat
    public GameObject leftStage;
    public GameObject rightStage;
    //temp health display
    public Text p_display;
    public Text e_display;
    void Start()
    {
        Instantiate(controller.GetPlayerBA(), leftStage.transform);
        Instantiate(controller.GetEnemyBA(), rightStage.transform);
    }

    void Update()
    {
        p_display.text = "HP: " + controller.GetPlayerHP();
        e_display.text = "HP: " + controller.GetEnemyHP();
        if (controller.GetState() == BattleState.START)
        {

        }

    }
}
