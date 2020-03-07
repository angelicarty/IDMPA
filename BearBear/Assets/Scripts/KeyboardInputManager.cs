using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : MonoBehaviour
{
    bool characterMoving;

    void Update()
    {
        if (characterMoving)
        {
            if (Input.GetKey("up"))
            {
                FindObjectOfType<CharacterMove>().moveUp();
            }
            else if (Input.GetKey("down"))
            {
                FindObjectOfType<CharacterMove>().moveDown();
            }
            else if (Input.GetKey("right"))
            {
                FindObjectOfType<CharacterMove>().moveRight();
            }
            else if (Input.GetKey("left"))
            {
                FindObjectOfType<CharacterMove>().moveLeft();
            }
            else
            {
                FindObjectOfType<CharacterMove>().notMoving();
            }
        }
    }

    public void disableCharacterMovement() //prevents character from moving
    {
        characterMoving = false;
    }

    public void enableCharacterMovement() //reenable character to move
    {
        characterMoving = true;
    }
}
