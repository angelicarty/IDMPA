using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{

    public Battle controller;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with something!" + other.name);
        Debug.Log(other.GetComponentInParent<Stats>().GetATK());
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Collision with player!");
            //Battle controller = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Battle>(); doesn't work, since hidden shit can't be found
            controller.over_player = other.gameObject;
            controller.over_enemy = this.gameObject;
            controller.gameObject.SetActive(true);
        }
    }
}
