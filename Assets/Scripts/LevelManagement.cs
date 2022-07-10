using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    
    [Header("Script References")]
    [SerializeField] private LevelProgressUI levelProgressUI;
    [SerializeField] private PlayerChild playerChild;
    [SerializeField] private Player player;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private DogParentPathMovement[] dogParentPathMovements;
    [SerializeField] private Dog[] dogs;
    
    
    [SerializeField] private GameObject[] levels;
    public int level;
    
    
  
    void Awake()
    {
        
        
        //PlayerPrefs.SetInt("level", 0);
        level = PlayerPrefs.GetInt("level");
        levelProgressUI.SetLevelTexts((level + 1));
        level = level % 6;
        levels[level].SetActive(true);
    }
    
    
    public void ScriptsInitiator()
    {
        levelProgressUI.enabled = true;
        playerChild.enabled = true;
        player.enabled = true;
        cameraFollow.enabled = true;
        foreach (var dogs in dogParentPathMovements)
        {
            dogs.enabled = true;
        }

        foreach (var dg in dogs)
        {
            
            dg.transform.GetComponent<Animator>().enabled = true;
            dg.enabled = true;
        }
        
        Debug.Log("Scripts are open.");

    }

    public void ScriptsTerminator()
    {
        levelProgressUI.enabled = false;
        playerChild.enabled = false;
        player.enabled = false;
        cameraFollow.enabled = false;
        foreach (var dogs in dogParentPathMovements)
        {
            dogs.enabled = false;
        }
        
        foreach (var dg in dogs)
        {
            dg.transform.GetComponent<Animator>().enabled = false;
            dg.enabled = false;
        }
        Debug.Log("Scripts are closed.");
    }
    
    
}


 
   
     
 
 
 

 
