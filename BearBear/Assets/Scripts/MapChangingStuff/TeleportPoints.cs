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
            FindObjectOfType<CameraFollowPlayer>().playerChangedMap();
            player.transform.position = teleportTo.transform.position;
        }
    }
}
