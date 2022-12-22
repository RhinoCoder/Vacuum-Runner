using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Ground : MonoBehaviour
{
    
    // Update is called once per frame
   
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("objToUp"))
        {
            other.transform.GetComponent<Rigidbody>().AddForce(Vector3.up*500f,ForceMode.Force);
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("objToUp"))
        {
            other.transform.GetComponent<Rigidbody>().AddForce(Vector3.up*500f,ForceMode.Force);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        float timer = 0;
        timer  = Time.deltaTime;
        
        if (other.gameObject.CompareTag("objToUp"))
        {
             other.transform.GetComponent<Rigidbody>().AddForce(Vector3.down*355f*timer,ForceMode.Force);
        }
        
    }

 
}
