using DG.Tweening;
using UnityEngine;
using PathCreation;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    
    [SerializeField] private PlayerChild PlayerChild;
    [SerializeField] private Camera mainCam;
    
    
    public PathCreator pathCreator;
    public bool canMove;
    public float speed = 4f;
    public float distanceTravelled = 5;
    public bool canShakeCamera;
    
    
    
    
    
    
    private void Update()
    {
        
        if (canMove)
        { 
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled,EndOfPathInstruction.Stop);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled,EndOfPathInstruction.Stop);
            //TODO to be developed
            if (canShakeCamera)
            {
                mainCam.transform.DOShakeRotation(0.5f,1f,10,90f,true);
                Debug.Log("Camera is shaked.");

            }
            
        }
        
        
    }
}