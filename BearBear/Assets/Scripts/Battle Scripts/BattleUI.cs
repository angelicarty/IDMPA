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
    //temp health display
    public Text p_display;
    public Text e_display;

    void OnEnable()
    {
        //Debug.Log("battle scene init thing loaded");
        Instantiate(controller.GetPlayer().battleActor, leftStage.transform);
        Instantiate(controller.GetEnemy().battleActor, rightStage.transform);
    }

    void OnDisable()
    {
        //Debug.Log("removing actors");
        Destroy(leftStage.transform.GetChild(0).gameObject);
        Destroy(rightStage.transform.GetChild(0).gameObject);
    }

    void Update()
    {
        p_display.text = "HP: " + controller.GetPlayerHP();
        e_display.text = "HP: " + controller.GetEnemyHP();
        if (controller.GetState() == BattleState.ACTION)
        {
            actions.SetActive(true);
        }
        else
        {
            actions.SetActive(false);
        }
    }
}
