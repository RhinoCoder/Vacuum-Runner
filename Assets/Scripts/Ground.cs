using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Ground : MonoBehaviour
{
    //Starts with around 0.6- 0.61
    private float startingDiff = 0f;
    [SerializeField] private GameObject objToUp;
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Starting Diff Update...." + startingDiff);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("objToUp"))
        {

            //ObjPosHandler(other.contacts[0].point,other.gameObject);
            other.transform.GetComponent<Rigidbody>().AddForce(Vector3.up*3f,ForceMode.Force);
            Debug.Log("Pulling them upp");
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("objToUp"))
        {
            //ObjPosHandler(other.contacts[0].point,other.gameObject);
            other.transform.GetComponent<Rigidbody>().AddForce(Vector3.up*15f,ForceMode.Force);
            Debug.Log("Pulling them upp");

 
        }
    }

    private void OnCollisionExit(Collision other)
    {
        float timer = 0;
        timer = Time.deltaTime;
        
        if (other.gameObject.CompareTag("objToUp"))
        {
             other.transform.GetComponent<Rigidbody>().AddForce(Vector3.down*25f*timer,ForceMode.Force);
             Debug.Log("Pulling them innn");

        }
        
    }


    private void ObjPosHandler(Vector3 curGround,GameObject ot)
    {
        /*
        var desiredHeight = curGround.y;
        var desiredY = Mathf.Lerp(transform.localPosition.y, curGround.y, 0.15f);
        */
        
        /*startingDiff = (ot.gameObject.transform.localPosition.y - curGround.y);
        var transformLocalPosition = transform.localPosition;
        transformLocalPosition.y = startingDiff;
        transform.transform.localPosition = transformLocalPosition;*/

    }
}
