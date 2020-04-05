using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    //TODO: divide this script into logical subgroups
    public GameObject menu_main;
    public GameObject menu_new;
    public GameObject menu_load;
    public InputField new_name;
    public Text new_error;
    private static int save_count = 3;
    private string[] saves = new string[save_count];
    private string[] init_saves()
    {//TODO fuck
        Debug.Log("LOADING SAVE FILES");
        string[] output = new string[save_count];
        int i = 0;
        foreach (string file in Directory.EnumerateFiles(@"c:\BearBear", "*.txt"))
        {
            if (i >= save_count) break;
            output[i] = RemoveFromEnd(file.Substring(file.LastIndexOf('\\') + 1), ".txt");
            Debug.Log(output[i++]);
        }
        return output;
    }

    private void OnEnable()
    {
        saves = init_saves();
    }

    public void exit()
    {

        //only works in builds
        Application.Quit();
    }

    public void start_button()
    {
        SceneManager.LoadScene("Map0");
    }

    public void new_game()
    {
        string input = new_name.text;
        bool failed = false;

        if (input == "") failed = true;
        //check for dupes
        foreach (string name in saves)
        {
            if (input.Equals(name))
            {
                new_error.text = "Sorry, that name is taken!";
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
            //TODO: create file and start game
            Debug.Log("NEW GAME PASSED, NOW FINISH THIS SCRIPT");
        }
        else
        {
            Debug.Log("Save creation failed");
        }


    }


    public void forward_new()
    {
        menu_main.SetActive(false);
        menu_new.SetActive(true);
    }

    public void back_new()
    {
        menu_main.SetActive(true);
        menu_new.SetActive(false);
    }

    public void forward_load()
    {
        menu_main.SetActive(false);
        menu_load.SetActive(true);
    }

    public void back_load()
    {
        menu_main.SetActive(true);
        menu_load.SetActive(false);
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
}
