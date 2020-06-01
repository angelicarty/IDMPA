using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public Dialogue[] appleDrops;
    public Dialogue[] noAppleDrops;
    public Dialogue[] appleButNoSpace;
    public float waitTime;
    public float timePassed;
    public GameObject apple;
    bool claimed = true;
    public GameObject dialogueBox;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("entered");
        {
            appleRNG();
            timePassed = 0f;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timePassed = 0f;
        }
    }

    void appleRNG()
    {
        timePassed = 0;
        int rng = Random.Range(1, 100);
        Debug.Log(rng);
        if (rng >= 90)
        {
            if(!FindObjectOfType<InventoryManager>().isInvFull())
            {
                gameObject.GetComponent<DialogueTrigger>().dialogues = appleDrops;
                gameObject.GetComponent<DialogueTrigger>().refreshDialogues();
                timePassed = 0f;
                rng = 0;
                claimed = false;
                Debug.Log(claimed);
            }
            else
            {
                gameObject.GetComponent<DialogueTrigger>().dialogues = appleButNoSpace;
                gameObject.GetComponent<DialogueTrigger>().refreshDialogues();
                claimed = false;
            }
        }
        else
        {
            gameObject.GetComponent<DialogueTrigger>().dialogues = noAppleDrops;
            gameObject.GetComponent<DialogueTrigger>().refreshDialogues();
        }

    }

    void giveApple()
    {
        int i = Random.Range(1, 3);
        if(FindObjectOfType<InventoryManager>().giveItemWithoutPrompt(apple, i))
        {
            Debug.Log(i);
            claimed = true;
            appleRNG();
            Debug.Log("claimed");
            timePassed = 0f;
        }
    }

    private void Update()
    {
        if(FindObjectOfType<MonstersController>().isInMobArea())
        {
            if (!dialogueBox.activeSelf)
            {
                timePassed += Time.deltaTime;
            }
            else if(dialogueBox.activeSelf)
            {
                if (!claimed)
                {
                    giveApple();
                }
            }
            if(timePassed > waitTime)
            {
                appleRNG();
            }
        }
    }

}
