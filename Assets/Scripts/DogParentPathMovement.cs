using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using Unity.VisualScripting;
using UnityEngine;

public class DogParentPathMovement : MonoBehaviour
{
    [SerializeField] private Dog dog;
    public PathCreator pathCreator;
    public bool canMove;
    public float speed = 4f;
    public float distanceTravelled = 5;

    
    private float timer = 0;
    
    private void Start()
    {
       
    }


    void Update()
    {
        if (canMove)
        {
            if (dog.isDogScared)
            {
                speed = 7f;
                timer += Time.deltaTime;
                
                if (timer >= 2f)
                {
                    
                    dog.isDogScared = false;
                    timer = 0;
                    Debug.Log("Dog Scared Bool Changed Back.");

                }
                
            }
            else
            {
                speed = 3.75f;
            }
            
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled,EndOfPathInstruction.Stop);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled,EndOfPathInstruction.Stop);
            
        }
    }


  
}