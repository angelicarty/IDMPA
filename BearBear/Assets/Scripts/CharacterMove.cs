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
    bool characterMoving = true;

    private void Update()
    {
        if (characterMoving)
        {
            if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
            {
                moveUp();
            }
            else if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
            {
                moveDown();
            }
            if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
            {
                moveRight();
            }
            else if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
            {
                moveLeft();
            }
            if (Input.GetKeyUp("up") || Input.GetKeyUp("w"))
            {
                notMovingUp();
            }
            if (Input.GetKeyUp("down") || Input.GetKeyUp("s"))
            {
                notMovingDown();
            }
            if (Input.GetKeyUp("right") || Input.GetKeyUp("d"))
            {
                notMovingRight();
            }
            if (Input.GetKeyUp("left") || Input.GetKeyUp("a"))
            {
                notMovingLeft();
            }
        }

    }

    private void FixedUpdate()
    {
        if (movingUp)
        {
            //character position change
            playerPosition = gameObject.transform.position;
            playerPosition.y = gameObject.transform.position.y + (walkingSpeed * Time.deltaTime); //prob eventually find a better maths calculation for walking speed 
            gameObject.transform.position = playerPosition;

        }
        if (movingDown)
        {
            //character position change
            playerPosition = gameObject.transform.position;
            playerPosition.y = gameObject.transform.position.y - (walkingSpeed * Time.deltaTime);
            gameObject.transform.position = playerPosition;

        }
        if (movingRight)
        {
            //character position change
            playerPosition = gameObject.transform.position;
            playerPosition.x = gameObject.transform.position.x + (walkingSpeed * Time.deltaTime);
            gameObject.transform.position = playerPosition;

        }
        if (movingLeft)
        {
            //character position change
            playerPosition = gameObject.transform.position;
            playerPosition.x = gameObject.transform.position.x - (walkingSpeed * Time.deltaTime);
            gameObject.transform.position = playerPosition;

        }
    }



    public void disableWalking()
    {
        GetComponent<SpriteRenderer>().flipX = false;
        characterMoving = false;
        movingDown = false;
        movingUp = false;
        movingRight = false;
        movingLeft = false;
        StopAllCoroutines();
    }

    public void enableWalking()
    {
        characterMoving = true;
        GetComponent<SpriteRenderer>().sprite = lookDown;

    }



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
            GetComponent<SpriteRenderer>().flipX = false;
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
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            //character sprite change
            GetComponent<SpriteRenderer>().sprite = lookRight;
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
            for (int i = 0; i < movingUpSprites.Length; i++)
            {
                yield return new WaitForSecondsRealtime(0.08f);

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

                    if (!movingUp)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = lookUp;
                        yield break;
                    }
                }
            }

        }
    }



    IEnumerator animateDown()
    {
        while (movingDown)
        {
            for (int i = 0; i < movingDownSprites.Length; i++)
            {
                yield return new WaitForSecondsRealtime(0.08f);


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
                yield return new WaitForSecondsRealtime(0.08f);


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
                yield return new WaitForSecondsRealtime(0.08f);

                gameObject.GetComponent<SpriteRenderer>().sprite = movingRightSprites[i];
                if (!movingLeft)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = lookRight;
                    yield break;
                }
            }
        }
    }
    

}
