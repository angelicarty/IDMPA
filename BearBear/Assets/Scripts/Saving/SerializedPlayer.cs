using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedPlayer : MonoBehaviour
{
    //Object used to save everything about the player
    public new string name;
    public string[] equipmentIDs;
    public string[] inventoryIds;
    public int[] inventoryCounts;
    public int gold;

    public string[] questTakenIds;
    public string[] questCompletedIds;
    public int[] questProgress;


    public int[] statValues = new int[7];
    public void GetInfo()
    {
        Stats statObj = gameObject.GetComponent<PlayerManager>().player.GetComponent<Stats>();
        name = statObj.name;
        statValues[0] = statObj.GetMHP();
        statValues[1] = statObj.GetCHP();
        statValues[2] = statObj.GetATK();
        statValues[3] = statObj.GetDEF();
        statValues[4] = statObj.GetSATK();
        statValues[5] = statObj.GetSDEF();
        statValues[6] = statObj.GetSPD();

        equipmentIDs = FindObjectOfType<EquipmentManager>().GetAllIds();
        inventoryIds = FindObjectOfType<InventoryManager>().GetAllIds();
        inventoryCounts = FindObjectOfType<InventoryManager>().GetAllCounts();
        gold = FindObjectOfType<InventoryManager>().GetGold();

        QuestManager qm = FindObjectOfType<QuestManager>();
        questTakenIds = qm.GetAllTakenIds();
        questCompletedIds = qm.GetAllCompletedIds();
        questProgress = qm.GetAllQuestProgress();


    }

    public void SetInfo()
    {
        Stats statObj = gameObject.GetComponent<PlayerManager>().player.GetComponent<Stats>();
        statObj.name = name;
        statObj.Reset();//just to be sure
        statObj.ModMHP(statValues[0]);
        statObj.SetCHP(statValues[1]);
        statObj.ModATK(statValues[2]);
        statObj.ModDEF(statValues[3]);
        statObj.ModSATK(statValues[4]);
        statObj.ModSDEF(statValues[5]);
        statObj.ModSPD(statValues[6]);

        InventoryManager inv = FindObjectOfType<InventoryManager>();

        inv.InitEquipment(equipmentIDs);
        inv.InitInventory(inventoryIds, inventoryCounts);
        inv.addGold(gold);

        QuestManager qm = FindObjectOfType<QuestManager>();
        qm.InitQuests(questTakenIds, questCompletedIds, questProgress);

    }
}
