using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuSaveLoad : MonoBehaviour
{
    FileContainer container;

    public InputField new_name;
    public Text new_error;
    public const int saveCount = 3;
    List<string> saves;
    public void NewGame()
    {

        string input = new_name.text;
        bool failed = false;

        if (input == "") failed = true;
        if (saves.Count >= saveCount)
        {
            new_error.text = "You can only have three save files! Delete one and try again";
            failed = true;
        }
        else
        {
            //check for dupes
            foreach (string name in saves)
            {
                if (input.Equals(name))
                {
                    new_error.text = "That name has already been used! Try a different one";
                    failed = true;
                    break;
                }
            }
            if (!failed)
            {
                //check for illegal characters
                if (input.Contains("/") || input.Contains("\\") || input.Contains(":") || input.Contains("*") || input.Contains("?") || input.Contains("\"") || input.Contains("<") || input.Contains(">") || input.Contains("|"))
                {
                    new_error.text = "Sorry, that name contains illegal characters!\nPlease don't use /, \\, :, *, ?, \", <, >, or |";
                    failed = true;
                }
            }
            if (!failed)
            {
                container.fileName = input;
                FindObjectOfType<LoadNewScene>().LoadScene();
            }
            else
            {
                Debug.Log("Save creation failed");
            }
        }


    }


    private List<string> InitSaves()
    {
        Debug.Log("LOADING SAVE FILES");
        List<string> output = new List<string>();
        int i = 0;
        string saveDirectory = RemoveFromEnd(SaveManager.GetPath(""), "/.json");
        Debug.Log("saveDirectory: " + saveDirectory);
        bool passed = false;
        do
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(saveDirectory, "*.json"))
                {
                    output.Add(RemoveFromEnd(file.Substring(file.LastIndexOf('\\') + 1), ".json"));
                    Debug.Log(output[i++]);
                }
                passed = true;
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(saveDirectory);
            }
        } while (!passed);
        return output;
    }

    void OnEnable()
    {
        container = FindObjectOfType<FileContainer>();
    }

    //suffix remove method: https://stackoverflow.com/questions/5284591/how-to-remove-a-suffix-from-end-of-string
    public string RemoveFromEnd(string s, string suffix)
    {
        if (s.EndsWith(suffix))
        {
            return s.Substring(0, s.Length - suffix.Length);
        }
        else
        {
            return s;
        }
    }

    public List<string> GetSaves()
    {
        saves = InitSaves();
        return saves;
    }
}
