using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("="))
        {
            for(int i =0; i < invSlots.Length; i++)
            {
                invSlots[i].isEmpty = false;
            }
        }
        if(Input.GetKeyDown("k"))
        {
            addGold(10);
        }
        if(Input.GetKeyDown("j"))
        {
            minusGold(2);
        }
    }

    public int purchaseItem(GameObject item, int count, int costPerObject)
    {
        if(gold > (count * costPerObject))
        {
            if(giveItem(item, count))
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

    public bool giveItem(GameObject item, int count)
    {
        if (item == null)
        {

            FindObjectOfType<KeyboardInputManager>().enableCharacterMovement(); //re-enable character movement
            //FindObjectOfType<MonstersController>().goingIntoMobArea(); //resume monster movements
            return true;

        }
        Debug.Log("giving: " + item.name);
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

    public void MoveCanvas(Canvas target)
    {//moves the inventory ui to a new canvas, for combat/overworld transition 
        inventoryPanel.transform.SetParent(target.transform, false);
    }

    public void EatItem(GameObject item)
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
                Destroy(invSlots[num - 1].slot.transform.GetChild(0).gameObject);

            }
            try
            {//in combat
                GameObject test = GameObject.FindGameObjectWithTag("BattleScene");//breaks if out of combat
                GameObject.FindGameObjectWithTag("BattleController").GetComponent<Battle>().ActionSelectItem(item);
            }
            catch (System.Exception e)
            {//out of combat
                playerStat.SetCHP(playerStat.GetCHP() + item.GetComponent<ItemProperties>().Use());
            }
        }
    }
}
