using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtons : MonoBehaviour
{
    public Battle controller;
    
    private int selectedAction = -1;

    public void Attack() 
    {
        if (controller.GetState().Equals(BattleState.ACTION))
        {
            controller.ActionAttack();
        }
    }

    public void Run()
    {
        if (controller.GetState().Equals(BattleState.ACTION))
        {
            controller.ActionRun();
        }
    }

    public void Special()
    { 
    //todo
    }

    public void Item()
    { 
    //todo
    }

    //for key controls
    private void Update()
    {
        if (controller.GetState().Equals(BattleState.ACTION))
        {
            if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && selectedAction > -1)
            {
                switch (selectedAction)
                {
                    case 0:
                        Attack();
                        break;
                    case 1:
                        Special();
                        break;
                    case 2:
                        Item();
                        break;
                    case 3:
                        Run();
                        break;
                }
                selectedAction = -1;
                controller.battleScene.GetComponent<BattleUI>().highlightButton(selectedAction);
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedAction = 0;
                controller.battleScene.GetComponent<BattleUI>().highlightButton(selectedAction);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectedAction = 1;
                controller.battleScene.GetComponent<BattleUI>().highlightButton(selectedAction);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedAction = 2;
                controller.battleScene.GetComponent<BattleUI>().highlightButton(selectedAction);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectedAction = 3;
                controller.battleScene.GetComponent<BattleUI>().highlightButton(selectedAction);
            }

        }
    }
}
