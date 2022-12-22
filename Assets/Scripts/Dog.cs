using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
 

public class Dog : MonoBehaviour
{
    
    
    public bool dogCanMove = true;
    public bool isDogScared = false;

    
    
    
    
    //Dog Components.
    [SerializeField] private Rigidbody dogRb;
    public Animator dogAnim;
    [SerializeField] private DogParentPathMovement dogParentPathMovement;
    
    
    [SerializeField] private GameObject fur;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private ParticleSystem exlMarkPart;
    [SerializeField] private AudioSource woofSound;
    [SerializeField] private ParticleSystem twinkleStars;
    
    private float timer = 0f;
    private float ts = 0f;

    public bool canDogContinue;
    private void Awake()
    {
        DOTween.Init();
    }

    private void Start()
    {
        
         
    }


    private void Update()
    {
        
        if (dogParentPathMovement.canMove && dogCanMove  )
        {
            MoveDog();
        }

        
    }


    
    
    
   

    private void AnimBoolChanger()
    {
        //dogAnim.SetBool("isAttacking",false);
        dogAnim.SetBool("run",true);
        dogParentPathMovement.canMove = true;
    }

    private void FurCreator()
    {

        float randX, randY, randZ,randScaleFactor,randPosX,randPosZ;
        randX = UnityEngine.Random.Range(0, 90f);
        randY = UnityEngine.Random.Range(0, 90f);
        randZ = UnityEngine.Random.Range(0, 120f);
        
        randScaleFactor = UnityEngine.Random.Range(0.75f, 2.85f);

        randPosX = UnityEngine.Random.Range(-0.1f, 0.3f);
        randPosZ = UnityEngine.Random.Range(-0.1f, 0.3f);
        
        GameObject newFur = Instantiate(fur, spawnTransform.position + new Vector3(randPosX,0.35f,randPosZ), Quaternion.Euler(randX,randY,randZ));
        newFur.transform.DOScale(randScaleFactor, 0.1f); 
    }


    private void MoveDog()
    {
        dogAnim.SetBool("run", true);
       
        timer += Time.deltaTime;

        if (timer > 0.06f)
        {
            FurCreator();
            timer = 0;
        }
    }
    
    //This will be called from the level manager script according to the level.
    public void SetAnimTrigger()
    {
        //dogAnim.SetTrigger("Level" + id);
        dogAnim.SetTrigger("Level1");
        transform.DOLookAt((transform.forward), 0.1f, AxisConstraint.Y);
    }

    private void DogScareHandler()
    {
        
        exlMarkPart.Play();
        woofSound.Play();
        Debug.Log("Dog is scared");

    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(dogParentPathMovement.gameObject,0.3f);
            Debug.Log("Köpek yok edildi.");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isDogScared = true;
            DogScareHandler();
        }


        if (other.gameObject.CompareTag("Obstacle"))
        {
         
            twinkleStars.Play();
            dogParentPathMovement.speed = 1.5f;
            dogAnim.speed = 0.5f;
            other.gameObject.tag = "Untagged";
        }
        else
        {
            AnimBoolChanger();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
         
            ts += Time.deltaTime;
        
            if (ts >= 0.2f)
            {
                dogParentPathMovement.speed = 6f;
                dogAnim.speed = 1.05f;
                ts = 0f;
                twinkleStars.Stop();
            }   
            
        }
         
    }


 
}