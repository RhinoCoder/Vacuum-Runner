using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    
    [Header("Script References")]
    [SerializeField] private LevelProgressUI levelProgressUI;
    [SerializeField] private PlayerChild playerChild;
    [SerializeField] private Player player;
    [SerializeField] private CameraParent cameraParent;
    [SerializeField] private Upgrade upgrade;
    
    
    [SerializeField] private List<DogParentPathMovement> dogParentPathMovements = new List<DogParentPathMovement>();
    [SerializeField] private List<Dog> dogs = new List<Dog>();
    
    
    public GameObject[] levels;
    public int level;
    
    
  
    void Awake()
    {
        
         
        level = PlayerPrefs.GetInt("level");
        levelProgressUI.SetLevelTexts((level + 1));
        level = level % 6;
        levels[level].SetActive(true);
        dogs = levels[level].gameObject.GetComponentsInChildren<Dog>().ToList();
        cameraParent.levelIndex = level;
        player.pathCreator = levels[level].gameObject.GetComponentInChildren<PathCreator>();
        dogParentPathMovements = levels[level].gameObject.GetComponentsInChildren<DogParentPathMovement>().ToList();
      
    }
    
    
    public void ScriptsInitiator()
    {
        levelProgressUI.enabled = true;
        playerChild.enabled = true;
        player.enabled = true;
        player.canMove = true;
        cameraParent.MoveCamera();
        player.speed = 4.5f;
        cameraParent.speed = 4.5f;
        foreach (var dgs in dogParentPathMovements)
        {
            dgs.enabled = true;
        }

        foreach (var dg in dogs)
        {
            
            dg.transform.GetComponent<Animator>().enabled = true;
            dg.enabled = true;
            dg.SetAnimTrigger();
            
        }
        
       

    }

    public void ScriptsTerminator()
    {
        
        levelProgressUI.enabled = false;
        playerChild.enabled = false;
        playerChild.vacuumEmitEffects.Stop();
        playerChild.emitParticles.Stop();
        upgrade.jetPack.GetComponent<Animator>().enabled = false;
        playerChild.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player.enabled = false;
        cameraParent.enabled = false;
      
        
        foreach (var dgs in dogParentPathMovements)
        {
            if (dgs){dgs.enabled = false;}
        }
        
        foreach (var dg in dogs)
        {
            if (dg)
            {
                dg.transform.GetComponent<Animator>().SetBool("run", false);
                dg.transform.GetComponent<Animator>().SetTrigger("sit");
                dg.transform.GetComponent<Animator>().enabled = false;
                dg.enabled = false;
            }
        }
        StopAllCoroutines();
    }

    
    
}


 
   
     
 
 
 

 
