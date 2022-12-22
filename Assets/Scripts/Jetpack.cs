using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{

    [SerializeField] private ParticleSystem[] jetPackParticles;
    
    

    private void OnEnable()
    {
        foreach (var jp in jetPackParticles)
        {
            jp.Play();
        }

    }

    private void OnDisable()
    {
        foreach (var jp in jetPackParticles)
        {
            jp.Stop();
        }

    }
}
