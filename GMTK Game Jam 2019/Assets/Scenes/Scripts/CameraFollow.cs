using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;


    private void FixedUpdate()
    {
        Vector2 desiredPosition = target.position;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3 (smoothedPosition.x, smoothedPosition.y, -10f);

        // transform.LookAt(target);
    }
}
