using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FileButtonProperties : MonoBehaviour
{
    public string nameStr = "<Empty>";
    public Text nameDis;
    public GameObject bear;
    public Button[] buttons;
    private string filePath;

    public void ResetDisplay()
    {
        nameStr = "<Empty>";
        nameDis.text = "<Empty>";
        bear.SetActive(false);
        buttons[0].interactable = false;
        buttons[1].interactable = false;
    }

    public void UpdateDisplay(string name)
    {
        nameStr = name;
        nameDis.text = name;
        bear.SetActive(true);
        buttons[0].interactable = true;
        buttons[1].interactable = true;
    }

    public void Delete()
    {
        filePath = SaveManager.GetPath(nameStr);
        File.Delete(filePath);
        ResetDisplay();
    }

    public void LoadGame()
    {
        if (nameStr != "<Empty>")
        {
            filePath = SaveManager.GetPath(nameStr);
            FindObjectOfType<FileContainer>().fileName = nameStr;
            FindObjectOfType<LoadNewScene>().LoadScene();
        }
    }
}
