using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [SerializeField] private Transform childRotation;

    void LateUpdate()
    {
        Vector3 desiredPosition = childRotation.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        //transform.LookAt(childRotation);
        
    }
    
}


