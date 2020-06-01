using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    bool followLR = true;
    bool followUD = true;
    Vector3 stopAt;
    Vector3 targetPosition;

    void Update()
    {
        if (followLR && followUD)
        {
            // Define a target position above the target transform
            targetPosition = target.TransformPoint(new Vector3(0, 0, -10));

            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else if(followUD)
        {
            targetPosition = target.TransformPoint(new Vector3(0, 0, -10));
            targetPosition.x = stopAt.x;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else if(followLR)
        {
            targetPosition = target.TransformPoint(new Vector3(0, 0, -10));
            targetPosition.y = stopAt.y;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

    }

    public void followLROff()
    {
        followLR = false;
        stopAt = transform.position;
    }

    public void followLROn()
    {
        followLR = true;
    }

    public void followUDOff()
    {
        followUD = false;
        stopAt = transform.position;
    }

    public void followUDOn()
    {
        followUD = true;
    }

    public void playerChangedMap()
    {
        transform.position = target.TransformPoint(new Vector3(0, 0, -10));

    }
}
