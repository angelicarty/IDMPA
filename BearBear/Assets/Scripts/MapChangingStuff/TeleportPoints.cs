using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoints : MonoBehaviour
{
    public GameObject teleportTo;
    public GameObject player;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine("MapTransition");

        }
    }

    IEnumerator MapTransition()
    {
        GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<Animator>().SetTrigger("Conceal");
        yield return new WaitForSecondsRealtime(1);
        FindObjectOfType<CameraFollowPlayer>().playerChangedMap();
        player.transform.position = teleportTo.transform.position;
        GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<Animator>().SetTrigger("Reveal");
    }
}
