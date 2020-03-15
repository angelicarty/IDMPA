using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : MonoBehaviour
{
    bool characterMoving = true; 

    void Update()
    {
        if (characterMoving)
        {
            if (Input.GetKeyDown("up"))
            {
                FindObjectOfType<CharacterMove>().moveUp();
            }
            else if (Input.GetKeyDown("down"))
            {
                FindObjectOfType<CharacterMove>().moveDown();
            }
            if (Input.GetKeyDown("right"))
            {
                FindObjectOfType<CharacterMove>().moveRight();
            }
            else if (Input.GetKeyDown("left"))
            {
                FindObjectOfType<CharacterMove>().moveLeft();
            }
            if(Input.GetKeyUp("up"))
            {
                FindObjectOfType<CharacterMove>().notMovingUp();
            }
            if(Input.GetKeyUp("down"))
            {
                FindObjectOfType<CharacterMove>().notMovingDown();
            }
            if (Input.GetKeyUp("right"))
            {
                FindObjectOfType<CharacterMove>().notMovingRight();
            }
            if (Input.GetKeyUp("left"))
            {
                FindObjectOfType<CharacterMove>().notMovingLeft();
            }
        }
        
        if (Input.GetKeyDown("space"))
        {
            FindObjectOfType<DialogueManager>().pressedSpace();
        }
    }

    public void disableCharacterMovement() //prevents character from moving
    {
        characterMoving = false;
        FindObjectOfType<CharacterMove>().notMovingUp();
        FindObjectOfType<CharacterMove>().notMovingDown();
        FindObjectOfType<CharacterMove>().notMovingRight();
        FindObjectOfType<CharacterMove>().notMovingLeft();
    }

    public void enableCharacterMovement() //reenable character to move
    {
        characterMoving = true;
    }
}
