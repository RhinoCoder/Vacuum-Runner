using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Upgrade : MonoBehaviour
{
    //  HOLY OOP FORGIVE ME.    

    [SerializeField] private Button vacuumEmitRadius;
    [SerializeField] private Button vacuumLifetime;
    [SerializeField] private Text costTextRadius;
    [SerializeField] private Text costTextLifetime;
    
    
    [SerializeField] private PlayerChild playerChild;
    [SerializeField] private LevelManagement levelManagement;
    
    
    public TMP_Text goldText;
    public Animator canvasAnim;
    
    [SerializeField] private int costRad =500;
    [SerializeField] private int costLife =500;
    [SerializeField] private Text fpsText;
    
    
    void Start()
    {
        costRad = PlayerPrefs.GetInt("costRad");
        costLife = PlayerPrefs.GetInt("costLife");
        costTextRadius.text = costRad.ToString();
        costTextLifetime.text = costLife.ToString();
        goldText.text = playerChild.goldAmount.ToString();
        
        //PlayerPrefs.SetInt("costRad",costRad);
        //PlayerPrefs.SetInt("costLife",costLife);
        
        
    }

    
    void Update()
    {
        fpsText.text = "FPS:" + (1 / Time.deltaTime).ToString();
    }


    public void startGame()
    {
        
        canvasAnim.SetTrigger("Start");
        levelManagement.ScriptsInitiator();   
        
        
    }

    public void retryLevel()
    {
        // call this
        
        SceneManager.LoadScene(0);

        
    }

    public void nextLevel()
    {
        
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        SceneManager.LoadScene(0);
       
    }

    public void UpgradeRadius()
    {
        if (costRad <= playerChild.goldAmount)
        {
            playerChild.emitCollider.radius += 0.001f;
            playerChild.emitParticles.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            playerChild.goldAmount -= costRad;
            costRad += 500;
            PlayerPrefs.SetInt("costRad",costRad);
            costTextRadius.text = costRad.ToString();
            goldText.text = playerChild.goldAmount.ToString();
            Debug.Log("Emitting radius is increased.");

        }
        
    }

    public void UpgradeLifetime()
    {
        if (costLife <= playerChild.goldAmount)
        {
            playerChild.vacuumLifetime += 250f;
            playerChild.goldAmount -= costLife;
            costLife += 500;
            PlayerPrefs.SetInt("costLife",costLife);
            costTextLifetime.text = costLife.ToString();
            goldText.text = playerChild.goldAmount.ToString();
            Debug.Log("Life time is increased.");

        }
        
    }
    
}
