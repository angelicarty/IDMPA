using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{

    public Dialogue welcomeDialogue;
    public Dialogue noMoneyDialogue;
    public Dialogue noSpaceDialogue;
    public Dialogue goodbyeDialogue;
    public Dialogue buyingDialogue;
    public GameObject[] itemsForSale;
    public Sprite shopkeeper;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<ShopManager>().triggerShop(welcomeDialogue, noMoneyDialogue, noSpaceDialogue,goodbyeDialogue,buyingDialogue , itemsForSale, shopkeeper);
            FindObjectOfType<KeyboardInputManager>().shopDialogueToggleShop();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            var shopManager = FindObjectOfType<ShopManager>();
            shopManager.outFromShop();
            FindObjectOfType<KeyboardInputManager>().shopDialogueToggleDialogue();

        }
    }
}
