using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class CameraParent : MonoBehaviour
{

    public float distanceTravelled = 5f;
    public float speed = 3.75f;
    public PathCreator[] pathCreator;
    public Player player;
    public Transform playerChild;

    public float lerpedXValue;
    public Vector3 lerpedPos;
    public Vector3 localOffset;
    public bool camMove;
    public int levelIndex;
    
    [SerializeField] private Camera mainCam;

    
  
   
    void Update()
    {
        if (camMove)
        {
            
            
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator[levelIndex].path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = pathCreator[levelIndex].path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            lerpedXValue = Mathf.Lerp(lerpedXValue, playerChild.transform.localPosition.x,10*Time.deltaTime);
            
            lerpedPos = Vector3.Lerp(lerpedPos, playerChild.transform.localPosition + localOffset, 0.5f*Time.deltaTime);
            mainCam.transform.localPosition = new Vector3(lerpedXValue, mainCam.transform.localPosition.y, mainCam.transform.localPosition.z);
            
           
        }
    }
    
    
    public void MoveCamera()
    {
        //lerpedXValue = transform.localPosition.x;
        lerpedXValue = playerChild.transform.localPosition.x;
        localOffset = transform.position -playerChild.transform.position;
        lerpedPos = playerChild.transform.localPosition + localOffset;
        camMove = true;
    }
}
