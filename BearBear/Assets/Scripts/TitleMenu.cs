using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void exit()
    {
        //only works in builds
        Application.Quit();
    }

    public void start_button()
    {
        SceneManager.LoadScene("Map0");
    }
}
