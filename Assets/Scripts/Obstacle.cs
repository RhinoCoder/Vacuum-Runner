using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{



    private Rigidbody obsRb;

    private void Awake()
    {
        obsRb = GetComponent<Rigidbody>();
    }

    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Dog"))
        {
            var dir = (transform.position - other.transform.position).normalized;
            obsRb.AddForce(-dir*150f,ForceMode.Impulse);
        }
    }
}
