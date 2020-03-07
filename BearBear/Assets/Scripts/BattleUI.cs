using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//controls the ui for the battle scene
public class BattleUI : MonoBehaviour
{
    public Battle controller;

    //draws the appropriate sprite for those involved in combat
    private GameObject p_sprite;
    private GameObject e_sprite;
    //temp health display
    public Text p_display;
    public Text e_display;
    void Start()
    {
        
    }

    void Update()
    {
        p_display.text = "HP: " + controller.GetPlayerHP();
        e_display.text = "HP: " + controller.GetEnemyHP();
        if (controller.GetState() == BattleState.START)
        {
            p_sprite.GetComponent<SpriteRenderer>().sprite = controller.GetPlayerSprite();
            e_sprite.GetComponent<SpriteRenderer>().sprite = controller.GetEnemySprite();
        }

    }
}
