using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    Battle controller;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision with something! " + other.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Collision with player!");
            controller = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Battle>();
            controller.over_player = other.gameObject;
            controller.over_enemy = this.gameObject;
            FindObjectOfType<KeyboardInputManager>().hideAllUi();
            FindObjectOfType<KeyboardInputManager>().disableCharacterMovement(); //disable character movement
            FindObjectOfType<KeyboardInputManager>().disableChat(); //prevents opening chat while in battle
            FindObjectOfType<MonstersController>().goingOutOfMobArea(); //pauses monster movements
            GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<Animator>().SetTrigger("Conceal");
            StartCoroutine("DelayedBattleStart");
        }
    }

    IEnumerator DelayedBattleStart()
    {
        yield return new WaitForSecondsRealtime(1);

        controller.InitBattle();
    }
}
