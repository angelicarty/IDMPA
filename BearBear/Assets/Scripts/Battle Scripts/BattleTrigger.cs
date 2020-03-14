using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision with something! " + other.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Collision with player!");

            Battle controller = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Battle>();
            controller.over_player = other.gameObject;
            controller.over_enemy = this.gameObject;
            controller.InitBattle();
        }
    }
}
