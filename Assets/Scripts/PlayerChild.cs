using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class PlayerChild : MonoBehaviour
{
    [SerializeField] private Transform inhalePoint;
    [SerializeField] private Player player;
    [SerializeField] private LevelManagement levelManagement;
    [SerializeField] private Animator canvasAnim;

    //Movement Related Variables.
    private float horizontal;
    private Vector3 mousePosition;
    private Rigidbody parentRb;
    private float startLifeTime;


    public float horizontalSpeed = 7f;
    public float vacuumLifetime = 1000f;
    public CapsuleCollider emitCollider;
    public int goldAmount;
    public int clampCase;
    public float goldFactor;
    public int newFactor;
    public ParticleSystem emitParticles;
    public ParticleSystem vacuumEmitEffects;


    [SerializeField] private Upgrade upgrade;
    [SerializeField] private Image lifeTimeSprite;
    [SerializeField] private GameObject objMove;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource failSound;
    [SerializeField] private TMP_Text furText;
    [SerializeField] private ParticleSystem[] collectEffects;


    private bool isFurCollecting;


    private void Awake()
    {
         
        parentRb = GetComponent<Rigidbody>();
        vacuumEmitEffects.Play();
        goldAmount = PlayerPrefs.GetInt("goldAmount");
       
    }


    void Start()
    {
        Application.targetFrameRate = 180;
        DOTween.Init();
        vacuumLifetime = PlayerPrefs.GetFloat("levelLifeTime");
        startLifeTime = vacuumLifetime;
    
        

    }

    
    void Update()
    {
        float vaccuumVal = vacuumLifetime;
        float progressVal = Mathf.InverseLerp(0f, startLifeTime, vaccuumVal);
        lifeTimeSprite.fillAmount = progressVal;


        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            vacuumLifeChecker();
        }
        else if (Input.GetMouseButton(0))
        {
            horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 2f;
            mousePosition = Input.mousePosition;
            vacuumLifetime -= 0.01f;
        }
        else
        {
            horizontal = 0;
        }

        if (player.canMove)
        {
            PlayerMove();
        }
    }

    private void PlayerMove()
    {
        vacuumLifeChecker();
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x + (horizontal * horizontalSpeed), -0.022f, 0.022f),
            transform.localPosition.y, transform.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fur"))
        {
            FurAbsorber(other);
            emitParticles.Play();
            player.playerParentAnim.SetBool("isEmitting", true);
        }
        else
        {
            player.playerParentAnim.SetBool("isEmitting", false);
            emitParticles.Stop();
        }

        if (other.gameObject.CompareTag("gold"))
        {
            //TODO add a collecting particle effect.
            other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            other.GetComponent<Gold>().SoundCaller();
            goldAmount += 50;
            PlayerPrefs.SetInt("goldAmount", goldAmount);
            upgrade.goldText.text = (goldAmount).ToString();
            Destroy(other.gameObject, 0.1f);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            player.canShakeCamera = true;
        }
        else
        {
            player.canShakeCamera = false;
        }


        if (other.gameObject.CompareTag("Finish"))
        {
            foreach (var ps in other.gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Play();
            }

            levelManagement.ScriptsTerminator();
            upgrade.NextLevelButt();
            winSound.Play();
            Debug.Log("Level is finished Here by winning, go to next Level.");
        }
    }


    private void FurAbsorber(Collider other)
    {
        collectEffects[0].Play();
        Vector3 dir = (other.gameObject.transform.position - inhalePoint.position).normalized;
        other.gameObject.transform.DOScale(0.02f, 0.35f);
        other.gameObject.GetComponent<Rigidbody>().AddForce(-dir * 50, ForceMode.Acceleration);
        Destroy(other.gameObject, 1f);
        vacuumLifetime -= 0.25f;
        goldFactor++;
        newFactor = Mathf.RoundToInt(goldFactor / 12);
        furText.text = newFactor.ToString();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            player.canShakeCamera = true;
        }

        else
        {
            player.canShakeCamera = false;
        }
    }


    private IEnumerator TrueMaker()
    {
        yield return new WaitForSeconds(0.2f);
        player.canMove = true;
    }

    private void vacuumLifeChecker()
    {
        if (vacuumLifetime <= 0)
        {
            upgrade.canvasAnim.SetTrigger("Retry");
            levelManagement.ScriptsTerminator();
            this.enabled = false;
            player.enabled = false;
            failSound.Play();
            PlayerPrefs.SetInt("goldAmount", goldAmount += newFactor);
        }
    }
}