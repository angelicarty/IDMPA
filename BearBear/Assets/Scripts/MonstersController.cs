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
    public LootTable[] lootTable;//weird 2d array, first index is for type of monster, second is a list of potential drops TODO this in a better way
    int counter;
    public Vector3[] mobsSpawnPoints;

    public float spawnTime;
    float currentTime;

    bool InMobArea = true;  //temp default to true for testing purpose

    public bool isInMobArea()
    {
        return InMobArea;
    }
    private void OnEnable()
    {
        //populate with mobs the moment the map loads (?)
        for (int i = 0; i < spawnedMobs.Length; i++)
        {
            counter = i;
            spawnMonster();
        }
        //and then freeze time till player comes into the map
        //goingOutOfMobArea();
    }

    void spawnMonster()
    {
        int monsterToSpawn = Random.Range(0, mobs.Length - 1);
        spawnedMobs[counter] = Instantiate(mobs[monsterToSpawn], mobsSpawnPoints[counter], Quaternion.identity);
        spawnedMobs[counter].GetComponent<MobDropContainer>().drop = lootTable[monsterToSpawn].loot[Random.Range(0, lootTable[monsterToSpawn].loot.Length)];
    }

    private void Update()
    {
        if (InMobArea)
        {
            if (checkSpawnSpots())
            {
                currentTime += Time.deltaTime;
            }
        }

        if (currentTime > spawnTime)
        {
            if (checkSpawnSpots())
            {
                spawnMonster();
                currentTime = 0f;
            }
        }

    }


    public void goingOutOfMobArea()
    {
        InMobArea = false;
        pausesMobMovement();
    }

    public void pausesMobMovement()
    {
        for (int i = 0; i < spawnedMobs.Length; i++)
        {
            if (spawnedMobs[i])
            {
                spawnedMobs[i].GetComponent<SlimeWalk>().slimeStopWalking();
            }
        }
    }

    public void resumeMobMovement()
    {
        for (int i = 0; i < spawnedMobs.Length; i++)
        {
            if (spawnedMobs[i])
            {
                spawnedMobs[i].GetComponent<SlimeWalk>().slimeWalking();
            }
        }
    }
    public void goingIntoMobArea()
    {
        if (InMobArea)
        {
            //nada
        }
        else
        {
            InMobArea = true;
            resumeMobMovement();
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

[System.Serializable]
public class LootTable
{
    public string mobName;
    public GameObject[] loot;
}