using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public InventorySlot[] invSlots;
    int gold;
    public GameObject goldCount;
    public Stats playerStat;
    public Dialogue itemAddedDialogue;
    int firstEmptyPosition = -1;
    bool itemAdded;
    string itemName;
    int itemCount;
    string sentence;
    public GameObject inventoryPanel;
    public GameObject testingItem;
    public EquipmentManager equipmentManager;
    public GameObject itemDescBox;
    public GameObject itemDescText;
    public GameObject itemNameText;

    float offsetX, offsetY;

    bool isMousedOverItem;

    //INFO BOX
    public void isMousedOver(string thisName, string desc)
    {
        isMousedOverItem = true;
        var childCount = itemDescBox.transform.parent.childCount;
        itemDescBox.transform.SetAsLastSibling();
        offsetX = -(itemDescBox.GetComponent<RectTransform>().sizeDelta.x + 10);
        offsetY = -(itemDescBox.GetComponent<RectTransform>().sizeDelta.y + 10) / 2;
        itemDescText.GetComponent<Text>().text = desc;
        itemNameText.GetComponent<Text>().text = thisName;
        mouseOverItem();
    }

    void mouseOverItem()
    {
        itemDescBox.SetActive(true);
        itemDescBox.transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y, 0);
    }

    public void isNotMousedOver()
    {
        isMousedOverItem = false;
        itemDescBox.SetActive(false);
    }

    private void LateUpdate()
    {
        if(isMousedOverItem)
        {
            mouseOverItem();
        }
    }
    
    //SHOP
    public int purchaseItem(GameObject item, int count, int costPerObject)
    {
        if(gold >= (count * costPerObject))
        {
            if(addItem(item, count))
            {
                minusGold(count * costPerObject);
                return 0; //no problem
            }
            return -1; //no space
        }
        return -2; //no money
    }

    public void addGold(int muns)
    {
        gold += muns;
        goldCount.GetComponent<UnityEngine.UI.Text>().text = gold.ToString();
    }

    public void minusGold(int muns)
    {
        if(gold > muns)
        {
            gold -= muns;
            goldCount.GetComponent<UnityEngine.UI.Text>().text = gold.ToString();
        }
    }

    public bool isInvFull()
    {
        for (int i = 0; i < invSlots.Length; i++)
        {
            if(invSlots[i].isEmpty)
            {
                return false; //at least one empty spot
            }
        }
        return true;
    }
    
    public void openInventory()
    {
        inventoryPanel.SetActive(true);
    }

    public void closeInventory()
    {
        isNotMousedOver();
        inventoryPanel.SetActive(false);
    }

    public bool questGiveItem(GameObject item, int count)
    {
        if (addItem(item, count))
        {
            //item received
            return true;
        }
        else
        {
            return false;
        }
    }


    public int getCount(GameObject item)
    {
        for (int i = 0; i < invSlots.Length; i++)
        {
            if(invSlots[i].slot.transform.GetChild(0).gameObject == item)
            {
                Debug.Log(invSlots[i].count);
                return invSlots[i].count;
            }
        }
        return 0;
    }

    public void removeItem(GameObject item,int count)
    {
        for (int i = 0; i < invSlots.Length; i++)
        {
            if (invSlots[i].slot.transform.GetChild(0).gameObject == item)
            {
                Debug.Log(invSlots[i].count);
                if(count < invSlots[i].count)
                {
                    invSlots[i].count -= count;
                    invSlots[i].countDisplay.GetComponent<Text>().text = invSlots[i].count.ToString();
                }
                else
                {
                    isNotMousedOver();
                    Destroy(invSlots[i].slot.transform.GetChild(0).gameObject);
                    invSlots[i].count = 0;
                    invSlots[i].countDisplay.SetActive(false);
                    invSlots[i].isEmpty = true;
                }
            }
        }
    }

    public bool giveItem(GameObject item, int count)
    {
        if (item == null)
        {

            FindObjectOfType<KeyboardInputManager>().enableCharacterMovement(); //re-enable character movement
            //FindObjectOfType<MonstersController>().goingIntoMobArea(); //resume monster movements
            return true;

        }
        if(addItem(item, count))
        {
            //item received
            itemAddedPrompt();
            return true;
        }
        else
        {
            //item not recieved due to inv being full
            sentence = "Oh no, your bag is full.";
            itemAddedDialogue.sentences[0] = sentence;
            FindObjectOfType<DialogueManager>().dialoguePrompt(itemAddedDialogue);
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
                        itemName = item.GetComponent<ItemProperties>().name;
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
                itemName = item.GetComponent<ItemProperties>().name;
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
        FindObjectOfType<MonstersController>().goingOutOfMobArea();
        sentence = "You obtained " + itemCount + " " + itemName;
        itemAddedDialogue.sentences[0] = sentence;
        FindObjectOfType<DialogueManager>().dialoguePrompt(itemAddedDialogue);
    }

    public void UseItem(GameObject item)
    {
        if (item.GetComponent<ItemProperties>().isEdible)
        {
            string slotname = EventSystem.current.currentSelectedGameObject.transform.parent.name;
            int num;
            int.TryParse(slotname, out num);
            invSlots[num - 1].count -= 1;
            invSlots[num - 1].countDisplay.GetComponent<UnityEngine.UI.Text>().text = invSlots[num - 1].count.ToString();
            if (invSlots[num - 1].count < 2)
            {
                invSlots[num - 1].countDisplay.SetActive(false);
            }
            if (invSlots[num - 1].count < 1)
            {
                invSlots[num - 1].isEmpty = true;
                isNotMousedOver();
                Destroy(invSlots[num - 1].slot.transform.GetChild(0).gameObject);

            }
            try
            {//in combat
                GameObject test = GameObject.FindGameObjectWithTag("BattleScene");//breaks if out of combat
                GameObject.FindGameObjectWithTag("BattleController").GetComponent<Battle>().ActionSelectItem(item.GetComponent<ItemProperties>());
            }
            catch (System.Exception)
            {//out of combat
                playerStat.SetCHP(playerStat.GetCHP() + item.GetComponent<ItemProperties>().Eat());
            }
        }
        else if (item.GetComponent<ItemProperties>().isEquipment)
        {
            string slotname = "1";
            try
            {//used for initating the equipment, it calls use without clicking so this breaks
                slotname = EventSystem.current.currentSelectedGameObject.transform.parent.name;
            }
            catch (System.Exception){ }
            int num;
            int.TryParse(slotname, out num);
            invSlots[num - 1].count -= 1;
            invSlots[num - 1].countDisplay.GetComponent<UnityEngine.UI.Text>().text = invSlots[num - 1].count.ToString();
            if (invSlots[num - 1].count < 2)
            {
                invSlots[num - 1].countDisplay.SetActive(false);
            }
            if (invSlots[num - 1].count < 1)
            {
                invSlots[num - 1].isEmpty = true;
                isNotMousedOver();
                Destroy(invSlots[num - 1].slot.transform.GetChild(0).gameObject);

            }



            switch (item.GetComponent<EquipmentProperties>().type)
            {
                case EquipType.HAND:

                    //Debug.Log("hand");
                    //places old item in inventory
                    if (equipmentManager.slot_hand.GetComponentInChildren<ItemProperties>() != null)
                    {
                        UnequipItem(equipmentManager.slot_hand.GetComponentInChildren<ItemProperties>().gameObject);
                    }

                    //equips new item
                    GameObject newHand = Instantiate(item, equipmentManager.slot_hand.transform, false);
                    newHand.transform.SetAsLastSibling();
                    newHand.GetComponent<Image>().enabled = true;
                    newHand.GetComponent<Button>().enabled = true;
                    newHand.GetComponent<MouseOverItem>().enabled = true;
                    break;

                case EquipType.HEAD:

                    //Debug.Log("head");
                    //places old item in inventory
                    if (equipmentManager.slot_head.GetComponentInChildren<ItemProperties>() != null)
                    {
                        UnequipItem(equipmentManager.slot_head.GetComponentInChildren<ItemProperties>().gameObject);
                    }

                    //equips new item
                    GameObject newHead = Instantiate(item, equipmentManager.slot_head.transform);
                   
                    newHead.transform.SetAsLastSibling();
                    newHead.GetComponent<Image>().enabled = true;
                    newHead.GetComponent<Button>().enabled = true;
                    newHead.GetComponent<MouseOverItem>().enabled = true;
                    newHead.name = item.name;
                    break;

                case EquipType.NECK:

                    //Debug.Log("neck");
                    //places old item in ;inventory
                    if (equipmentManager.slot_neck.GetComponentInChildren<ItemProperties>() != null)
                    {
                        UnequipItem(equipmentManager.slot_neck.GetComponentInChildren<ItemProperties>().gameObject);
                    }

                    //equips new item
                    GameObject newNeck = Instantiate(item, equipmentManager.slot_neck.transform, false);
                    newNeck.transform.SetAsLastSibling();
                    newNeck.GetComponent<Image>().enabled = true;
                    newNeck.GetComponent<Button>().enabled = true;
                    newNeck.GetComponent<MouseOverItem>().enabled = true;
                    newNeck.name = item.name;
                    break;
            }

            try
            {//in combat
                GameObject test = GameObject.FindGameObjectWithTag("BattleScene");//breaks if out of combat
                GameObject.FindGameObjectWithTag("BattleController").GetComponent<Battle>().ActionSelectItem(item.GetComponent<ItemProperties>());
            }
            catch (System.Exception)
            {
                Debug.Log("Item equipped out of combat");
                //out of combat, do nothing
            }
        }
    }

    public void UnequipItem(GameObject item)
    {
        //adds item to inventory, removes from equipment slot
        item.name = item.name.Replace("(Clone)", "");
        if (addItem(item, 1))
        {
            isNotMousedOver();
            Destroy(item);
        }
        
    }
    public bool isInvEmpty()
    {
        for(int i=0; i < invSlots.Length; i++)
        {
            if(invSlots[i].isEmpty == false)
            {
                return false;
            }
        }
        return true;
    }

    //gets all item IDs and quantities for saving
    public string[] GetAllIds()
    {
        string[] output = new string[invSlots.Length];
        for (int i = 0; i < invSlots.Length; i++)
        {
            if (invSlots[i].isEmpty)
            {
                output[i] = "-";
            }
            else
            { 
                output[i] = invSlots[i].slot.transform.GetChild(0).gameObject.name.Replace("(Clone)", "");

            }
        }
        return output;
    }

    //gets the quantity of items in each slot
    public int[] GetAllCounts()
    {
        int[] output = new int[invSlots.Length];
        for (int i = 0; i < invSlots.Length; i++)
        {
            if (invSlots[i].isEmpty)
            { 
                output[i] = 0;
            }
            else
            {
                output[i] = getCount(invSlots[i].slot.transform.GetChild(0).gameObject);
            }
        }
        return output;
    }

    //for loading, adds the equipped equipment to inventory and uses it
    public void InitEquipment(string[] IDs)
    {
        ItemList list = FindObjectOfType<ItemList>();

        for (int i = 0; i < IDs.Length; i++)
        {
            if (IDs[i] != "-")
            {
                foreach (GameObject item in list.Items)
                {
                    if (IDs[i] == item.name)
                    {
                        addItem(item, 1);
                        UseItem(item);
                    }
                }
            }
        }
    }

    //for loading, adds the equipped equipment to inventory and uses it
    public void InitInventory(string[] IDs, int[] counts)
    {
        ItemList list = FindObjectOfType<ItemList>();
        int num;

        for (int i = 0; i < IDs.Length; i++)
        {
            if(IDs[i] != "-") 
            { 
                foreach (GameObject item in list.Items)
                {
                    if (IDs[i] == item.name)
                    {
                        
                        addItem(item, counts[i]);
                    }
                }
            }
        }
    }

    public int GetGold()
    {
        return gold;
    }
}
