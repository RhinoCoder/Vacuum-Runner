using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathCreation;
using Unity.VisualScripting;
using UnityEngine;

public class DogParentPathMovement : MonoBehaviour
{
    [SerializeField] private Dog dog;
    [SerializeField] private Animator dogAnim;
    [SerializeField] private ParticleSystem twinkleEffect;

    
    public PathCreator pathCreator;
    public bool canMove;
    public float speed = 4f;
    public float distanceTravelled = 5;


    private float timer = 0f;



    
    void Update()
    {
        if (canMove)
        {
            
            if (dog.isDogScared)
            {
                speed = 10f;
                timer += Time.deltaTime;
                dog.dogAnim.speed = 1.45f;
                
                if (timer >= 2f)
                {
                    dog.isDogScared = false;
                    speed = 5f;
                    dog.dogAnim.speed = 1f;
                    timer = 0;
                }
            }
         

            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
        }
    }
}