using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<MonstersController>().goingOutOfMobArea();
            FindObjectOfType<VolControl>().inTown();
        }
    }
}
