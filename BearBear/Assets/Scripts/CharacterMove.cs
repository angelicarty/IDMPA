using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{

    Vector3 playerPosition;
    public Sprite lookUp,lookDown,lookRight,lookLeft;
    bool movingUp, movingDown, movingLeft, movingRight;

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

    public void notMoving()
    {

    }

    IEnumerator animateUp()
    {
        while(movingUp)
        {
            yield return new WaitForSeconds(0.02f); //will change depending on drames
        }
    }



}
