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
    public void New_game()
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
                //TODO: create file and start game
                Debug.Log("NEW GAME PASSED, NOW FINISH THIS SCRIPT");
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
        foreach (string file in Directory.EnumerateFiles(@"c:\BearBear", "*.json"))
        {
            output.Add(RemoveFromEnd(file.Substring(file.LastIndexOf('\\') + 1), ".json"));
            Debug.Log(output[i++]);
        }
        return output;
    }

    void OnEnable()
    {
        container = FindObjectOfType<FileContainer>();
        saves = InitSaves();
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
