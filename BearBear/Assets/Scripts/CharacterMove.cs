using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{

    Vector3 playerPosition;
    public Sprite lookUp,lookDown,lookRight,lookLeft;
    bool movingUp, movingDown, movingLeft, movingRight;
    public Sprite[] movingRightSprites;
    public Sprite[] movingDownSprites;
    public Sprite[] movingUpSprites;
    //public GameObject followCharacterCamera;

    public void moveUp()
    {
        if (!movingDown)
        {
            //character sprite change
            //GetComponent<SpriteRenderer>().sprite = lookUp;
            movingUp = true;
            StartCoroutine(animateUp());
        }
    }
    public void moveDown()
    {
        if (!movingUp)
        {
            //character sprite change
            //GetComponent<SpriteRenderer>().sprite = lookDown;
            movingDown = true;
            StartCoroutine(animateDown());
        }
    }
    public void moveRight()
    {
        if (!movingLeft)
        {
            //character sprite change
            GetComponent<SpriteRenderer>().sprite = lookRight;
            movingRight = true;
            StartCoroutine(animateRight());
        }
    }
    public void moveLeft()
    {
        if (!movingRight)
        {
            //character sprite change
            GetComponent<SpriteRenderer>().sprite = lookLeft;
            movingLeft = true;
            StartCoroutine(animateLeft());
        }
    }

    public void notMovingUp()
    {
        movingUp = false;
        StopCoroutine(animateUp());
    }
    public void notMovingDown()
    {
        movingDown = false;
        StopCoroutine(animateDown());
    }
    public void notMovingLeft()
    {
        movingLeft = false;
        StopCoroutine(animateLeft());
    }
    public void notMovingRight()
    {
        movingRight = false;
        StopCoroutine(animateRight());
    }

    IEnumerator animateUp()
    {
        while (movingUp)
        {
            //character position change
            playerPosition = gameObject.transform.position;
            playerPosition.y = gameObject.transform.position.y + (10 * Time.deltaTime); //prob eventually find a better maths calculation for walking speed 
            gameObject.transform.position = playerPosition;
            yield return new WaitForSeconds(0.08f);

            //temp in comment till frames available
            /*
            for (int i = 0; i < movingDownSprites.Length; i++)
            {
                yield return new WaitForSeconds(0.08f);

                //character position change
                playerPosition = gameObject.transform.position;
                playerPosition.y = gameObject.transform.position.y + (10 * Time.deltaTime); //prob eventually find a better maths calculation for walking speed 
                gameObject.transform.position = playerPosition;

                if (movingLeft || movingRight)
                {
                    //do nothing on sprite
                    if (!movingUp)
                    {
                        yield break;
                    }
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = movingUpSprites[i];

                    if (!movingDown)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = lookUp;
                        yield break;
                    }
                }
            }*/

        }
    }

    IEnumerator animateDown()
    {
        while (movingDown)
        {
            for (int i = 0; i < movingDownSprites.Length; i++)
            {
                yield return new WaitForSeconds(0.08f);

                //character position change
                playerPosition = gameObject.transform.position;
                playerPosition.y = gameObject.transform.position.y - (10 * Time.deltaTime);
                gameObject.transform.position = playerPosition;

                if (movingLeft || movingRight)
                {
                    //do nothing on sprite
                    if (!movingDown)
                    {
                        yield break;
                    }
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = movingDownSprites[i];

                    if (!movingDown)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = lookDown;
                        yield break;
                    }
                }
            }
        }
    }

    IEnumerator animateRight()
    {
        while (movingRight)
        {
            for (int i = 0; i < movingRightSprites.Length; i++)
            {
                yield return new WaitForSeconds(0.08f);

                //character position change
                playerPosition = gameObject.transform.position;
                playerPosition.x = gameObject.transform.position.x + (10 * Time.deltaTime);
                gameObject.transform.position = playerPosition;

                gameObject.GetComponent<SpriteRenderer>().sprite = movingRightSprites[i];
                if (!movingRight)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = lookRight;
                    yield break;
                }
            }
        }
    }

    IEnumerator animateLeft()
    {
        while (movingLeft)
        {
            for (int i = 0; i < movingRightSprites.Length; i++)
            {
                yield return new WaitForSeconds(0.08f);

                //character position change
                playerPosition = gameObject.transform.position;
                playerPosition.x = gameObject.transform.position.x - (10 * Time.deltaTime);
                gameObject.transform.position = playerPosition;

                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = movingRightSprites[i];
                if (!movingLeft)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = lookLeft;
                    yield break;
                }
            }
        }
    }


}
