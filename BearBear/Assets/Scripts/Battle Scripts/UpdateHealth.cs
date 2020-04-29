using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealth : MonoBehaviour
{
    public float scale = 1;
    //used to set the new scale of the health bar

    IEnumerator UpdateHealthBar()
    {
        Debug.Log("UPDATING HEALTH BAR: " + scale);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.localScale = new Vector3(scale, 1, 1);
    }
}
