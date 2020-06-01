using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] ui_menus;



    public void MoveCanvas(Canvas target)
    {//moves the ui to a new canvas, for combat/overworld transition 
        foreach (GameObject ui in ui_menus)
        { 
            ui.transform.SetParent(target.transform, false);
        } 
    }

    public void ClosePauseMenu()
    { 
        FindObjectOfType<KeyboardInputManager>().ClosePauseMenu();
    }
}
