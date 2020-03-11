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
    public GameObject[] spawnedMobs;
    int counter;
    public Vector3[] mobsSpawnPoints;

    public float spawnTime;
    float currentTime;

    bool InMobArea = true;  //temp default to true for testing purpose

    void spawnMonster()
    {
        int monsterToSpawn = Random.Range(0, mobs.Length - 1);
        spawnedMobs[counter] = Instantiate(mobs[monsterToSpawn], mobsSpawnPoints[counter], Quaternion.identity);
    }

    private void Update()
    {
        if (InMobArea)
        {
            currentTime += Time.deltaTime;
        }

        if(currentTime > spawnTime)
        {
            if(checkSpawnSpots())
            {
                spawnMonster();
                currentTime = 0f;
            }
        }
    }

    public void goingIntoMobArea()
    {
        InMobArea = true;
        for (int i = 0; i < spawnedMobs.Length; i++)
        {
            spawnedMobs[i].GetComponent<SlimeWalk>().slimeStopWalking();
        }
    }

    public void goingOutOfMobArea()
    {
        InMobArea = false;
        for (int i = 0; i < spawnedMobs.Length; i++)
        {
            spawnedMobs[i].GetComponent<SlimeWalk>().slimeWalking();
        }
    }

    private bool checkSpawnSpots()
    {
        for(int i = 0; i < mobsSpawnPoints.Length; i++)
        {
            if(spawnedMobs[i] == null)
            {
                counter = i;
                return true;
            }
        }
        return false;
    }


    /*
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
    }*/
}
