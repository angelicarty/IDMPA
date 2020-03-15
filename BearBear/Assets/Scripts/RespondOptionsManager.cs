using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespondOptionsManager : MonoBehaviour
{
    public GameObject respondBox;
    Dialogue[] replies;

    public void acceptReplies(Dialogue[] replies)
    {

    }

    public void pickAReply(int chosenOPtion)
    {
        switch(chosenOPtion)
        {
            case 0:
                Debug.Log("choosen one");
                break;
            case 1:
                Debug.Log("choosen 2");
                break;
            default:
                Debug.Log("bloop");
                break;
        }
    }

}
