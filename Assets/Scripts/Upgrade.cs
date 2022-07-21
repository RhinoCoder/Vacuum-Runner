using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    
    
    [SerializeField] private TMP_Text levelTextRadius;
    [SerializeField] private TMP_Text levelTextLifetime;
    
    

    [SerializeField] private PlayerChild playerChild;
    [SerializeField] private LevelManagement levelManagement;
    [SerializeField] private Player player;

    public TMP_Text goldText;
    public Animator canvasAnim;
    public GameObject jetPack;
    
    
    [SerializeField] private int costRad = 500;
    [SerializeField] private int costLife = 500;
    [SerializeField] private ParticleSystem upgradeParticle;

    public int xp;

    private int levelRadius;
    private int levelLifeTime;
    
    

    void Start()
    {
        
        //PlayerPrefs.DeleteAll();
        
        
        costRad = PlayerPrefs.GetInt("costRad");
        costLife = PlayerPrefs.GetInt("costLife");
        xp = PlayerPrefs.GetInt("xp");
        levelRadius = PlayerPrefs.GetInt("levelRadius");
        levelLifeTime = PlayerPrefs.GetInt("levelLifeTime");
        
        
        
        costTextRadius.text = costRad.ToString();
        costTextLifetime.text = costLife.ToString();
        goldText.text = playerChild.goldAmount.ToString();
     
        
      
        
        //PlayerPrefs.SetInt("costRad",costRad);
        //PlayerPrefs.SetInt("costLife",costLife);
        //PlayerPrefs.SetInt("xp", xp);
    }


    void Update()
    {
         
    }


    public void startGame()
    {
        if (xp > 8)
        {
            jetPack.SetActive(true);
        }

        canvasAnim.SetTrigger("Start");
        levelManagement.ScriptsInitiator();
        player.canMove = true;
    }

    public void retryLevel()
    {
       
        SceneManager.LoadScene(0);
    }

    public void nextLevel()
    {
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        SceneManager.LoadScene(0);
    }

    public void NextLevelButt()
    {
        canvasAnim.SetTrigger("Next");
        
    }
    
    public void UpgradeRadius()
    {
        if (costRad <= playerChild.goldAmount && playerChild.emitCollider.radius <= 0.02f)
        {
            playerChild.emitCollider.radius += 0.001f;
            playerChild.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            playerChild.emitParticles.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            playerChild.goldAmount -= costRad;
            costRad += 500;
            PlayerPrefs.SetInt("costRad", costRad);
            PlayerPrefs.SetInt("xp",xp++);
            PlayerPrefs.SetInt("levelRadius",levelRadius++);
            costTextRadius.text = costRad.ToString();
            goldText.text = playerChild.goldAmount.ToString();
            levelTextRadius.text = "LEVEL "+levelRadius.ToString();
            upgradeParticle.Play();
            
            Debug.Log("Emitting radius is increased.");
        }
    }

    public void UpgradeLifetime()
    {
        //TODO this 4000 can be changed.
        if (costLife <= playerChild.goldAmount && playerChild.vacuumLifetime <= 4000)
        {
            playerChild.vacuumLifetime += 250f;
            playerChild.goldAmount -= costLife;
            costLife += 500;
            PlayerPrefs.SetInt("costLife", costLife);
            PlayerPrefs.SetInt("xp",xp++);
            PlayerPrefs.SetInt("levelLifeTime",levelLifeTime++);
            costTextLifetime.text = costLife.ToString();
            goldText.text = playerChild.goldAmount.ToString();
            levelTextLifetime.text= "LEVEL " +levelLifeTime.ToString();
            upgradeParticle.Play();
            Debug.Log("Life time is increased.");
        }
    }
    
    
    
}