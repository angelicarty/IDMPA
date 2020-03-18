using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : MonoBehaviour
{
    bool characterMovement;
    void Update()
    {
        if (characterMovement)
        {
            if (Input.GetKeyDown("space"))
            {
                FindObjectOfType<DialogueManager>().pressedSpace();
            }
        }
    }

    public void disableCharacterMovement() //prevents character from moving
    {
        characterMovement = false;
        FindObjectOfType<CharacterMove>().disableWalking();
    }

    public void enableCharacterMovement() //reenable character to move
    {
        characterMovement = true;
        FindObjectOfType<CharacterMove>().enableWalking();
    }
}
