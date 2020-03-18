using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            FindObjectOfType<DialogueManager>().pressedSpace();
        }
    }

    public void disableCharacterMovement() //prevents character from moving
    {
        FindObjectOfType<CharacterMove>().disableWalking();
    }

    public void enableCharacterMovement() //reenable character to move
    {
        FindObjectOfType<CharacterMove>().enableWalking();
    }
}
