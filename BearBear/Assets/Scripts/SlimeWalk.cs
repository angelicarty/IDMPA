using UnityEngine;

public class SlimeWalk : MonoBehaviour
{

    public Sprite[] slimeWalk = new Sprite[4];

    public void slimeWalking() //calls when player get out of battle, or when scene loads
    {
        //pink a random direction to go to
        //either right,left, left up, left down 
        //no straight up or down cuz that requires more assets to be drawn lol

        //calls for coroutine to move to that direction
    }
}
