using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileDisplay : MonoBehaviour
{
    public MenuSaveLoad loadManager;
    private List<GameObject> fileButtons;
    public GameObject fileButtonPrefab;
    public Transform scrollbox;
    private void OnEnable()
    {
        string[] saves = loadManager.GetSaves();
        fileButtons = new List<GameObject>();
        GameObject temp;
        foreach (string save in saves)
        {
            temp = Instantiate(fileButtonPrefab, scrollbox);
            temp.GetComponentInChildren<Text>().text = save;
            temp.GetComponent<FileButtonProperties>().name = save;
            fileButtons.Add(temp);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject file in fileButtons)
        {
            Destroy(file);
        }
    }
}
