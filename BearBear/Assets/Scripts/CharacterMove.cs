using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
     Vector3 playerPosition;
    public Sprite lookUp,lookDown,lookRight,lookLeft;
    public void moveUp()
    {
        //character position change
        playerPosition = gameObject.transform.position;
        playerPosition.y = gameObject.transform.position.y + (1 * Time.deltaTime);
        gameObject.transform.position = playerPosition;

        //character sprite change
        GetComponent<SpriteRenderer>().sprite = lookUp;
    }
    public void moveDown()
    {
        //character position change
        playerPosition = gameObject.transform.position;
        playerPosition.y = gameObject.transform.position.y - (1 * Time.deltaTime);
        gameObject.transform.position = playerPosition;

        //character sprite change
        GetComponent<SpriteRenderer>().sprite = lookDown;
    }
    public void moveRight()
    {
        //character position change
        playerPosition = gameObject.transform.position;
        playerPosition.x = gameObject.transform.position.x + (1 * Time.deltaTime);
        gameObject.transform.position = playerPosition;

        //character sprite change
        GetComponent<SpriteRenderer>().sprite = lookRight;
    }
    public void moveLeft()
    {
        //character position change
        playerPosition = gameObject.transform.position;
        playerPosition.x = gameObject.transform.position.x - (1 * Time.deltaTime);
        gameObject.transform.position = playerPosition;

        //character sprite change
        GetComponent<SpriteRenderer>().sprite = lookLeft;
    }


    //animation stuff yet to be done but here's the code anyway 
    /*
     * 
     *IEnumerator animateUp()
     * {
     *    for (int i = 1; i < lookUp.Length; i++) //length is the number of frames for look up animation
          {
              yield return new WaitForSeconds(0.18f); //change number to match however many frames there is and how long each change between frame should be
              GetComponent<SpriteRenderer>().sprite = lookUp[i];
          }
     * }
     * 
     * 
     * public void stopAnimation()
     * {
     *    stopCoroutine(animateup());
     * }
     * 
     * */

}
