using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
//    public GameObject player_prefab; TODO: make this thing spawn in the player
    public GameObject player;
    public SaveManager saver;
    public Battle battleController;
    void Start()
    {
        //player = Instantiate(player_prefab);
        saver.player = player.GetComponent<Stats>();
        saver.LoadPlayer();
        battleController.overworld_camera = player.GetComponentInChildren<Camera>();
    }
}
