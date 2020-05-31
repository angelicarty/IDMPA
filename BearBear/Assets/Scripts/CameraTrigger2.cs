using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<CameraFollowPlayer>().followUDOff();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<CameraFollowPlayer>().followUDOn();
        }
    }
}
