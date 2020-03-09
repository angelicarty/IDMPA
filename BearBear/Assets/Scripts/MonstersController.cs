using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersController : MonoBehaviour
{
    //script that spawns monster, stops monster movement and resume them
    //every 10 seconds, it'll check if there's a monster occupying every spawn point
    //if there's an empty spot, spawn a monster there, one monster spawns per 10 seconds
    //begins the map with fully spawned monsters tho


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
