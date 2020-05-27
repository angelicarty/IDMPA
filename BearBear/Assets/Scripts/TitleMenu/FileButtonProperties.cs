using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FileButtonProperties : MonoBehaviour
{
    public string name;
    private string filePath;

    private void OnEnable()
    {
        string filePath = @"c:\BearBear\" + name + ".json";

    }
    public void Delete()
    {
        File.Delete(filePath);
        Destroy(gameObject);
    }

    public void LoadGame()
    {
        FindObjectOfType<FileContainer>().fileName = name;
        FindObjectOfType<LoadNewScene>().LoadScene();
    }
}
