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
    public float walkingSpeed;
    public GameObject followCharacterCamera;
    Vector3 cameraCoords;


    private void moveCamera()
    {
        cameraCoords.x = playerPosition.x;
        cameraCoords.y = playerPosition.y;
        followCharacterCamera.transform.position = cameraCoords;
    }

    public void moveUp()
    {
        if (!movingDown)
        {
            //character sprite change
            //GetComponent<SpriteRenderer>().sprite = lookUp;
            movingUp = true;
            StartCoroutine(animateUp());
            StartCoroutine(walkingUp());
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
            StartCoroutine(walkingDown());
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
            StartCoroutine(walkingRight());
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
            StartCoroutine(walkingLeft());
        }
    }

    public void notMovingUp()
    {
        movingUp = false;
        StopCoroutine(animateUp());
        StopCoroutine(walkingUp());
    }
    public void notMovingDown()
    {
        movingDown = false;
        StopCoroutine(animateDown());
        StopCoroutine(walkingDown());
    }
    public void notMovingLeft()
    {
        movingLeft = false;
        StopCoroutine(animateLeft());
        StopCoroutine(walkingLeft());
    }
    public void notMovingRight()
    {
        movingRight = false;
        StopCoroutine(animateRight());
        StopCoroutine(walkingRight());
    }

    IEnumerator animateUp()
    {
        while (movingUp)
        {
            
            yield return new WaitForSeconds(0.08f);

            //temp in comment till frames available
            /*
            for (int i = 0; i < movingDownSprites.Length; i++)
            {
                yield return new WaitForSeconds(0.08f);

                //character position change
                playerPosition = gameObject.transform.position;
                playerPosition.y = gameObject.transform.position.y + (walkingSpeed * Time.deltaTime); //prob eventually find a better maths calculation for walking speed 
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

    IEnumerator walkingUp()
    {
        while (movingUp)
        {
            //character position change
            playerPosition = gameObject.transform.position;
            playerPosition.y = gameObject.transform.position.y + (walkingSpeed * Time.deltaTime); //prob eventually find a better maths calculation for walking speed 
            gameObject.transform.position = playerPosition;
            moveCamera();
            yield return null;
        }
    }

    IEnumerator animateDown()
    {
        while (movingDown)
        {
            for (int i = 0; i < movingDownSprites.Length; i++)
            {
                yield return new WaitForSeconds(0.08f);


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

    IEnumerator walkingDown()
    {
        while (movingDown)
        {
            //character position change
            playerPosition = gameObject.transform.position;
            playerPosition.y = gameObject.transform.position.y - (walkingSpeed * Time.deltaTime);
            gameObject.transform.position = playerPosition;
            moveCamera();
            yield return null;
        }
    }

    IEnumerator animateRight()
    {
        while (movingRight)
        {
            for (int i = 0; i < movingRightSprites.Length; i++)
            {
                yield return new WaitForSeconds(0.08f);


                gameObject.GetComponent<SpriteRenderer>().sprite = movingRightSprites[i];
                if (!movingRight)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = lookRight;
                    yield break;
                }
            }
        }
    }

    IEnumerator walkingRight()
    {
        while (movingRight)
        {
            //character position change
            playerPosition = gameObject.transform.position;
            playerPosition.x = gameObject.transform.position.x + (walkingSpeed * Time.deltaTime);
            gameObject.transform.position = playerPosition;
            moveCamera();
            yield return null;
        }
    }

    IEnumerator animateLeft()
    {
        while (movingLeft)
        {
            for (int i = 0; i < movingRightSprites.Length; i++)
            {
                yield return new WaitForSeconds(0.08f);

                

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
    
    IEnumerator walkingLeft()
    {
        while (movingLeft)
        {
            //character position change
            playerPosition = gameObject.transform.position;
            playerPosition.x = gameObject.transform.position.x - (walkingSpeed * Time.deltaTime);
            gameObject.transform.position = playerPosition;
            moveCamera();
            yield return null;
        }
    }

}
