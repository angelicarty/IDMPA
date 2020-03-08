using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersController : MonoBehaviour
{
    //script that spawns monster, stops monster movement and resume them

    public GameObject[] mobs;
    int counter;

    void spawnMonster()
    {
        mobs[counter] = Instantiate(mob1, new Vector3(0, 0, 0), Quaternion.identity);
    }





    //temp - tested and working
    public GameObject mob1, mob2;

    private void Update()
    {
        if(Input.GetKeyDown("k"))
        {
            mob1.GetComponent<SlimeWalk>().slimeStopWalking();
            mob2.GetComponent<SlimeWalk>().slimeWalking();
        }
        if (Input.GetKeyDown("j")) 
        {
            mob2.GetComponent<SlimeWalk>().slimeStopWalking();
            mob1.GetComponent<SlimeWalk>().slimeWalking();
        }
    }
}
