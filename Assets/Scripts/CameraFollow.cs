using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using PathCreation;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using DG.Tweening;


public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.250f;
    public Vector3 offset;
    public float distanceTravelled = 5f;
    public float speed = 5f;
    public int levelIndex;


    
    [SerializeField] private Transform childRotation;
    [SerializeField] private LevelManagement levelManagement;
    [SerializeField] private Player player;
     
    [SerializeField] private PathCreator pathCreator;
    
    private void Awake()
    {
        
    }

    private void Start()
    {
        
        Debug.Log("Level Index" + levelIndex);
        DOTween.Init();

    }

 
    
    void LateUpdate()
    {

        if (player.canMove)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled,EndOfPathInstruction.Stop)+offset;
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled,EndOfPathInstruction.Stop);
            
        }
        

        
        
        //Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;
        //transform.LookAt(target);
         


    }
}