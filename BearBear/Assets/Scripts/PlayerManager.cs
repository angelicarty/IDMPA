using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
//    public GameObject player_prefab; TODO: make this thing spawn in the player
    public GameObject player;
    public SaveManager saver;
    public Battle battleController;
    public Vector3 spawnLocation;
    void Start()
    {
        //player = Instantiate(player_prefab);
        player.transform.position = spawnLocation;
        saver.player = player.GetComponent<Stats>();
        saver.LoadPlayer();
        battleController.overworld_camera = player.GetComponentInChildren<Camera>();
    }

    public void Death()
    {
        player.transform.position = spawnLocation;
        player.GetComponent<Stats>().SetCHP((int)System.Math.Floor((double)player.GetComponent<Stats>().GetMHP() / 2));
    }


}
