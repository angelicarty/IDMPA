using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public GameObject characterStatsUI;
    public Stats playerStats;
    public GameObject hpText;
    public GameObject ATKText;
    public GameObject defText;
    public GameObject spdText;

    public void displayStats()
    {
        characterStatsUI.SetActive(true);
        statsUIUpdate();
    }

    private void statsUIUpdate()
    {
        hpText.GetComponent<Text>().text = playerStats.GetCHP() + "/" + playerStats.GetMHP();
        ATKText.GetComponent<Text>().text = playerStats.GetATK().ToString();
        defText.GetComponent<Text>().text = playerStats.GetDEF().ToString();
        spdText.GetComponent<Text>().text = playerStats.GetSPD().ToString();
    }

    public void hideStats()
    {
        characterStatsUI.SetActive(false);
    }
}
