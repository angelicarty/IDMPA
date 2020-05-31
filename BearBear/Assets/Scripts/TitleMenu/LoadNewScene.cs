using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public int sceneNumber;
    private bool quit;
    public void LoadScene()
    {
        GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<Animator>().SetTrigger("Conceal");
        quit = false;
        StartCoroutine("DelayedExit");
    }

    public void Exit()
    {
        GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<Animator>().SetTrigger("Conceal");
        quit = true;
        StartCoroutine("DelayedExit");
    }

    IEnumerator DelayedExit()
    {
        yield return new WaitForSeconds(1);
        if (quit)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }

}
