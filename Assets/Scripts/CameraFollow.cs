using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using PathCreation;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float distanceTravelled = 5f;


    [SerializeField] private Transform childRotation;
    [SerializeField] private LevelManagement levelManagement;
   
    public int levelIndex;

    private void Awake()
    {


    }

    private void Start()
    {
        Debug.Log("Level Index" + levelIndex);

    }


    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);

    }
}