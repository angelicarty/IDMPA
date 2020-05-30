using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Stats player;
    public SerializedPlayer data;
    FileContainer container;
    string fileName;

    void OnEnable()
    {
        container = FindObjectOfType<FileContainer>();
        if (container == null)
        {
            fileName = "test";
        }
        else
        {
            fileName = container.fileName;
        }
    }

    public static string GetPath(string saveID)
    {
        //TODO: make this dynamic
        return Application.dataPath + "/Saves/" + saveID + ".json";
    }

    public void LoadPlayer()
    {
        Load(fileName);
        
    }

    public void SavePlayer()
    {
        Save(fileName);
    }

    public void Load(string saveID)
    {
        string path = GetPath(saveID);
        if (!File.Exists(path))
        {
            //starting a new game, load defaults
            using (StreamWriter sw = File.CreateText(path))
            {
                data.SetDefaultInfo(saveID);
                data.GetInfo();
                string output = JsonUtility.ToJson(data);
                sw.WriteLine(output);
            }

            //Debug.LogError("FILE " + saveID + " NOT FOUND");
        }
        else
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string input = sr.ReadLine();
                JsonUtility.FromJsonOverwrite(input, data);
                data.SetInfo();
            }
        }
    }

    public void Save(string saveID)
    {
        string path = GetPath(saveID);
        using (StreamWriter sw = File.CreateText(path))
        {
            data.GetInfo();
            string output = JsonUtility.ToJson(data);
            sw.WriteLine(output);
        }
    }
}
