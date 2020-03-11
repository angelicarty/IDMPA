using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlimeWalk : MonoBehaviour
{

    public Sprite[] slimeWalk;
    Vector3 slimePosition;
    int direction; //1 = left, 2 = right, 3 = up-left, 4 = down-left, 5 = up-right, 6 = down-right
    bool walks = true;
    float directionCD = 4f;
    float directionTimer = 0.0f;
    int walkSpriteCounter = 0;
    public float walkingSpeed;
    //Vector3 initialPosition;

    private void OnEnable()
    {
        slimePosition = gameObject.transform.position;
        //initialPosition = slimePosition;
        newDirection();
        slimeWalking(); //temp
    }

    public void slimeWalking() //calls when player get out of battle, or when scene loads
    {
        walks = true;
        StartCoroutine(itWalks());
    }

    public void slimeStopWalking()
    {
        walks = false;
        StopCoroutine(itWalks());
    }

    void Update() 
    {
        if (walks)
        {
            directionTimer += Time.deltaTime;
            if (directionTimer >= directionCD)
            {
                newDirection();
                directionTimer = 0;
            }
        }
    }

    IEnumerator itWalks() //will improve on walking AI (turning when colliding, random direction and all that)
    {
        while (walks)
        {
            for (int i = walkSpriteCounter; i < slimeWalk.Length; i++)
            {
                yield return new WaitForSeconds(0.2f);
                gameObject.GetComponent<SpriteRenderer>().sprite = slimeWalk[i];
                slimePosition = gameObject.transform.position;

                switch(direction)
                {
                    case 1: //left
                        slimePosition.x = gameObject.transform.position.x - (walkingSpeed * Time.deltaTime); 
                        break;
                    case 2: //right
                        slimePosition.x = gameObject.transform.position.x + (walkingSpeed * Time.deltaTime);
                        break;
                    case 3: //up-left
                        slimePosition.x = gameObject.transform.position.x - (walkingSpeed * Time.deltaTime);
                        slimePosition.y = gameObject.transform.position.y + (walkingSpeed * Time.deltaTime);
                        break;
                    case 4: //down-left
                        slimePosition.x = gameObject.transform.position.x - (walkingSpeed * Time.deltaTime);
                        slimePosition.y = gameObject.transform.position.y - (walkingSpeed * Time.deltaTime);
                        break;
                    case 5: //up-right
                        slimePosition.x = gameObject.transform.position.x + (walkingSpeed * Time.deltaTime);
                        slimePosition.y = gameObject.transform.position.y + (walkingSpeed * Time.deltaTime);
                        break;
                    case 6: //down-right
                        slimePosition.x = gameObject.transform.position.x + (walkingSpeed * Time.deltaTime);
                        slimePosition.y = gameObject.transform.position.y - (walkingSpeed * Time.deltaTime);
                        break;
                    default:
                        break;
                }
                gameObject.transform.position = slimePosition;
                walkSpriteCounter = i;
                if (!walks)
                {
                    yield break;
                }
            }
            walkSpriteCounter = 0;
            yield return new WaitForSeconds(0.6f);
        }
    }


    void newDirection() 
    {
        direction = Random.Range(1, 6);
        //Debug.Log(direction);
        if (direction == 1 || direction == 3 || direction == 4) //facing left
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else //facing right
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        //also changes how long more till the next directional change to increase randomness
        directionCD = Random.Range(3.0f, 5.0f);

        //keep it within range of spawn point
        /*
        
        float dist = Vector3.Distance(other.position, transform.position);

        if(dist > someRange )
        {
            direction = moveTowardsSpawnPoint();
        }
        */
    }
}
