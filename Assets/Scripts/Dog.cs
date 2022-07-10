using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    
    
    public bool dogCanMove = true;
    public bool isDogScared = false;

    
    
    
    
    //Dog Components.
    [SerializeField] private Rigidbody dogRb;
    [SerializeField] private Animator dogAnim;
    [SerializeField] private DogParentPathMovement dogParentPathMovement;
    
    
    [SerializeField] private GameObject fur;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private ParticleSystem exlMarkPart;
    [SerializeField] private AudioSource woofSound;    
    
    
    private float timer = 0;


    private void Awake()
    {
        DOTween.Init();
    }

    void Start()
    { 
        SetAnimTrigger(1);
    }


    private void Update()
    {
        if (dogCanMove)
        {
            MoveDog();
        }
    }

    private void AttackObstacle()
    {
        dogParentPathMovement.canMove = false;
        dogAnim.SetBool("isAttacking",true);
    }

    private void AnimBoolChanger()
    {
        dogAnim.SetBool("isAttacking",false);
        dogAnim.SetBool("run",true);
        dogParentPathMovement.canMove = true;
    }

    private void FurCreator()
    {

        float randX, randY, randZ,randScaleFactor,randPosX,randPosZ;
        randX = UnityEngine.Random.Range(0, 90f);
        randY = UnityEngine.Random.Range(0, 90f);
        randZ = UnityEngine.Random.Range(0, 120f);
        
        randScaleFactor = UnityEngine.Random.Range(0.15f, 1.5f);

        randPosX = UnityEngine.Random.Range(-0.1f, 0.3f);
        randPosZ = UnityEngine.Random.Range(-0.1f, 0.3f);
        
        GameObject newFur = Instantiate(fur, spawnTransform.position + new Vector3(randPosX,0.35f,randPosZ), Quaternion.Euler(randX,randY,randZ));
        newFur.transform.DOScale(randScaleFactor, 0.1f);
        Debug.Log("Fur Emmitted..");
    }


    private void MoveDog()
    {
        dogAnim.SetBool("run", true);
       
        timer += Time.deltaTime;

        if (timer > 0.01f)
        {
            FurCreator();
            timer = 0;
        }
    }
    
    //This will be called from the level manager script according to the level.
    public void SetAnimTrigger(int id)
    {
        dogAnim.SetTrigger("Level" + id);
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
            AttackObstacle();
            other.gameObject.tag = "Untagged";
        }
        else
        {
            AnimBoolChanger();
        }
    }
}