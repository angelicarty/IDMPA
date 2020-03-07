using UnityEngine;
using System.Collections;

public class SlimeWalk : MonoBehaviour
{

    public Sprite[] slimeWalk = new Sprite[4];
    Vector3 slimePosition;

    private void OnEnable()
    {
        slimePosition = gameObject.transform.position;
        slimeWalking(); //temp
    }

    public void slimeWalking() //calls when player get out of battle, or when scene loads
    {
        StartCoroutine(itWalks());
    }

    public void slimeStopWalking()
    {
        StopCoroutine(itWalks());
    }

    IEnumerator itWalks() //will improve on walking AI (turning when colliding, random direction and all that)
    {
        while (true)
        {
            for (int i = 0; i < slimeWalk.Length; i++)
            {
                yield return new WaitForSeconds(0.2f);
                gameObject.GetComponent<SpriteRenderer>().sprite = slimeWalk[i];
                slimePosition = gameObject.transform.position;
                slimePosition.x = gameObject.transform.position.x + (10 * Time.deltaTime);
                gameObject.transform.position = slimePosition;
            }
            for(int j = slimeWalk.Length; j > 0; j--)
            {
                yield return new WaitForSeconds(0.2f);
                gameObject.GetComponent<SpriteRenderer>().sprite = slimeWalk[j-1];
                slimePosition = gameObject.transform.position;
                slimePosition.x = gameObject.transform.position.x + (10 * Time.deltaTime);
                gameObject.transform.position = slimePosition;
            }
        }
    }


}
