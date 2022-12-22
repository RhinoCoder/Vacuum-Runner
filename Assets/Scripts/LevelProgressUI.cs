using System;
using UnityEngine ;
using UnityEngine.UI ;

public class LevelProgressUI : MonoBehaviour {

   [Header ("UI references :")]
   public Image uiFillImage ;
   public Text uiStartText ;
   public Text uiEndText ;

   [Header ("Player & Endline references :")]
   public Transform playerTransform ;
 

   // "endLinePosition" to cache endLine's position to avoid
   // "endLineTransform.position" each time since the End line has a fixed position.
   private Vector3 endLinePosition ;

   // "fullDistance" stores the default distance between the player & end line.
   private float fullDistance ;


   private void Awake()
   {
      endLinePosition = GameObject.FindGameObjectWithTag("Finish").transform.position;
      
   }

   private void Start () {
      fullDistance = GetDistance () ;
   }


   public void SetLevelTexts (int level) {
      uiStartText.text = level.ToString () ;
      uiEndText.text = (level + 1).ToString () ;
   }


   private float GetDistance () 
   {
      return Vector3.Distance (playerTransform.position, endLinePosition) ;
   }


   private void UpdateProgressFill (float value)
   {
      uiFillImage.fillAmount = value ;
   }


   private void Update () {
      // check if the player doesn't pass the End Line
      if (playerTransform.position.z <= endLinePosition.z) {
         float newDistance = GetDistance () ;
         float progressValue = Mathf.InverseLerp (fullDistance, 0f, newDistance) ;
         UpdateProgressFill (progressValue) ;
      }
   }

  
}
