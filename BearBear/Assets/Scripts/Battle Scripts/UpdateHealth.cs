using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealth : MonoBehaviour
{
    public float scale = 0;//target scale
    private float speed = 4f;
    public bool reset = false;
    private float duration = 1f;
    //used to set the new scale of the health bar

    IEnumerator UpdateHealthBar()
    {
        if (reset)
        {
            gameObject.transform.localScale = new Vector3(0, 1, 1);
            reset = false;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            yield return RepeatLerp(gameObject.transform.localScale, new Vector3(scale, 1, 1), duration);


            //Debug.Log("UPDATING HEALTH BAR: " + scale);
        }

    }

    IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0f;
        float rate = (1f / time) * speed;
        while (i < 1f)
        {
            i += Time.deltaTime * rate;
            gameObject.transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
