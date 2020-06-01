using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTutorial : MonoBehaviour
{
    public GameObject[] tutorials;
    int pagenum =0;
    public void nextPage()
    {
        tutorials[pagenum].SetActive(false);
        pagenum++;
        if(pagenum > tutorials.Length - 1)
        {
            pagenum = 0;
        }
        tutorials[pagenum].SetActive(true);
    }

    public void prevPage()
    {
        tutorials[pagenum].SetActive(false);
        pagenum--;
        if (pagenum < 0)
        {
            pagenum = tutorials.Length - 1;
        }
        tutorials[pagenum].SetActive(true);
    }
}
