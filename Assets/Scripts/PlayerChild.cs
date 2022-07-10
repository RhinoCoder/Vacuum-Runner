using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerChild : MonoBehaviour
{

    [SerializeField] private Transform inhalePoint;
    [SerializeField] private Player player;
    
    
    //Movement Related Variables.
    private float horizontal, vertical;
    private Vector3 mousePosition;
    private Rigidbody parentRb;
    
    
    
    public float horizontalSpeed = 3f;
    public float vacuumLifetime=1000f;
    public CapsuleCollider emitCollider;
    public int goldAmount;
    
    

    public ParticleSystem emitParticles;
    public ParticleSystem vacuumEmitEffects;
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private GameObject lifeTimeSprite;
    

    private void Awake()
    {
        parentRb = GetComponent<Rigidbody>();
        vacuumEmitEffects.Play();
        //PlayerPrefs.SetInt("goldAmount",goldAmount);
        goldAmount = PlayerPrefs.GetInt("goldAmount");

    }


    void Start()
    {
        Application.targetFrameRate = 60;
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }
    
    private void PlayerMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width*2f;
            mousePosition = Input.mousePosition;
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }




      
        
        transform.localPosition  = new Vector3(
            Mathf.Clamp(transform.localPosition.x + (horizontal * horizontalSpeed), -0.035f, 0.035f),
            transform.localPosition.y, transform.localPosition.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fur"))
        {
            FurEmitter(other);
            emitParticles.Play();
        }
        else
        {
            emitParticles.Stop();
        }
        
        if (other.gameObject.CompareTag("gold"))
        {
            //TODO add a collecting particle effect.
            other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            other.GetComponent<Gold>().SoundCaller();
            goldAmount += 50;
            PlayerPrefs.SetInt("goldAmount",goldAmount);
            upgrade.goldText.text = (goldAmount).ToString();            
            Destroy(other.gameObject,0.1f);
            Debug.Log("Gold taken.");
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            player.canShakeCamera=true;
        }
        else
        {
            player.canShakeCamera = false;
        }
    }

    private void FurEmitter(Collider other)
    {
        Vector3 dir = (other.gameObject.transform.position - inhalePoint.position).normalized;
        other.gameObject.transform.DOScale(0.02f, 0.35f);
        other.gameObject.GetComponent<Rigidbody>().AddForce(-dir * 50, ForceMode.Acceleration);
        Destroy(other.gameObject, 1f);
        vacuumLifetime -= 0.25f;
        Debug.Log("The fur is inhaled");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("wall"))
        {

            var pos = transform.position + new Vector3(-5f,0f,0f);
            transform.DOMove(pos, 0.25f, false);
            player.canShakeCamera = true;
        }
        else
        {
            player.canShakeCamera = false;
        }
    }
}
