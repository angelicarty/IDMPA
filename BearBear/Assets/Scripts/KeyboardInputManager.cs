using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : MonoBehaviour
{
    bool characterChat = true;
    void Update()
    {
        if (characterChat)
        {
            if (Input.GetKeyDown("space"))
            {
                FindObjectOfType<DialogueManager>().pressedSpace();
            }
        }
    }

    public void disableCharacterMovement() //prevents character from moving
    {
        FindObjectOfType<CharacterMove>().disableWalking();
    }

    public void disableChat()
    {
        characterChat = false;
    }

    public void enableChat()
    {
        characterChat = true;
    }

    public void enableCharacterMovement() //reenable character to move
    {
        FindObjectOfType<CharacterMove>().enableWalking();
    }
}
