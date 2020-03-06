using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : MonoBehaviour
{
    bool moved = false;

    void Update()
    {
        if(Input.GetKey("up"))
        {
            FindObjectOfType<CharacterMove>().moveUp();
            //moved = true
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
        else if(moved)
        {
            //FindObjectOfType<CharacterMove>().stopAnimation();
        }
    }
}
