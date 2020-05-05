using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{

    public Dialogue welcomeDialogue;
    public Dialogue noMoneyDialogue;
    public Dialogue noSpaceDialogue;
    public Dialogue goodbyeDialogue;
    public GameObject[] itemsForSale;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<ShopManager>().triggerShop(welcomeDialogue, noMoneyDialogue, noSpaceDialogue,goodbyeDialogue, itemsForSale);
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<ShopManager>().resetShop();
        }
    }
}
