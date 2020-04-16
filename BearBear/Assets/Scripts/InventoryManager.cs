﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{

    public InventorySlot[] invSlots;
    public GameObject apple;
    public GameObject apple2;
    public Stats playerStat;
    public Dialogue itemAddedDialogue;
    int firstEmptyPosition = -1;
    bool itemAdded;
    string itemName;
    int itemCount;
    string sentence;
    public GameObject inventoryPanel;

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown("k"))
        {
            addItem(apple,10);
        }
        if(Input.GetKeyDown("j"))
        {
            addItem(apple2, 1);
        }
    }
    
    public void openInventory()
    {
        inventoryPanel.SetActive(true);
    }

    public void closeInventory()
    {
        inventoryPanel.SetActive(false);
    }

    public bool giveItem(GameObject item, int count)
    {
        if(addItem(item, count))
        {
            //item received
            itemAddedPrompt();
            return true;
        }
        else
        {
            //item not recieved due to inv being full
            sentence = "Your bag is full, maybe empty it up before talking to them again";
            itemAddedDialogue.sentences[0] = sentence;
            FindObjectOfType<DialogueManager>().addedItem(itemAddedDialogue);
            return false;
        }
    }

    bool addItem(GameObject item, int count)
    {
        firstEmptyPosition = -1;
        itemAdded = false;
        for(int i = 0; i<invSlots.Length;i++)
        {
            if (!invSlots[i].isEmpty)
            {
                var occupant = invSlots[i].slot.transform.GetChild(0).gameObject;
                int numOfLettersOfOccupant = occupant.name.Length;
                if (item.name.Length <= occupant.name.Length)
                {
                    string itemInSlot = occupant.name.Substring(0, item.name.Length);
                    if (item.name == itemInSlot)
                    {
                        invSlots[i].count += count;
                        invSlots[i].countDisplay.GetComponent<UnityEngine.UI.Text>().text = invSlots[i].count.ToString();
                        if(invSlots[i].count >= 2)
                        {
                            invSlots[i].countDisplay.SetActive(true);
                        }
                        itemName = item.name;
                        itemCount = count;
                        itemAdded = true;
                        return true;
                    }
                }
            }
            else if (invSlots[i].isEmpty)
            {
                if(firstEmptyPosition < 0)
                    firstEmptyPosition = i;
            }
        }
        if (!itemAdded)
        {
            if (firstEmptyPosition >= 0)
            {
                Instantiate(item, invSlots[firstEmptyPosition].slot.transform, false).transform.SetAsFirstSibling();
                invSlots[firstEmptyPosition].count += count;
                invSlots[firstEmptyPosition].countDisplay.GetComponent<UnityEngine.UI.Text>().text = invSlots[firstEmptyPosition].count.ToString();
                invSlots[firstEmptyPosition].isEmpty = false;
                if (invSlots[firstEmptyPosition].count >= 2)
                {
                    invSlots[firstEmptyPosition].countDisplay.SetActive(true);
                }
                itemAdded = true;
                itemName = item.name;
                itemCount = count;
                firstEmptyPosition = -1;
                return true;
            }
            else
            {
                //no empty slot
                return false;
            }
        }
        return false;
    }

    void itemAddedPrompt()
    {
        sentence = "You obtained " + itemCount + " " + itemName;
        itemAddedDialogue.sentences[0] = sentence;
        FindObjectOfType<DialogueManager>().addedItem(itemAddedDialogue);
    }

    public void MoveCanvas(Canvas target)
    {//moves the inventory ui to a new canvas, for combat/overworld transition 
        inventoryPanel.transform.SetParent(target.transform, false);
    }

    public void EatItem(GameObject item)
    {
        string slotname = EventSystem.current.currentSelectedGameObject.transform.parent.name;
        int num;
        int.TryParse(slotname, out num);
        invSlots[num - 1].count -= 1;
        invSlots[num - 1].countDisplay.GetComponent<UnityEngine.UI.Text>().text = invSlots[num - 1].count.ToString();
        try
        {//in combat
            GameObject test = GameObject.FindGameObjectWithTag("BattleScene");//breaks if out of combat
            GameObject.FindGameObjectWithTag("BattleController").GetComponent<Battle>().ActionSelectItem(item);
        }
        catch (System.Exception e)
        {//out of combat
            playerStat.SetCHP(playerStat.GetCHP() + item.GetComponent<Edible>().Use());
        }
    }
}
