using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins;
using UnityEngine;

public class Gold : MonoBehaviour
{

    [SerializeField] private float rotSpeed = 15f;

    private AudioSource collectSound;

    private void Start()
    {
        DOTween.Init();
        collectSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up*Time.deltaTime*rotSpeed);
    }

    public void SoundCaller()
    {
        collectSound.Play();   
    }
    
    
}
