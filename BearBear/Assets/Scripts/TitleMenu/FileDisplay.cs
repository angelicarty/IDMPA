using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileDisplay : MonoBehaviour
{
    public MenuSaveLoad loadManager;
    public List<FileButtonProperties> fileButtons;
    public GameObject fileButtonPrefab;
    private void OnEnable()
    {
        List<string> saves = loadManager.GetSaves();
        for(int i = 0; i < saves.Count; i++)
        {
            fileButtons[i].UpdateDisplay(saves[i]);
        }
    }

    private void OnDisable()
    {
        foreach (FileButtonProperties file in fileButtons)
        {
            file.ResetDisplay(); ;
        }
    }
}
