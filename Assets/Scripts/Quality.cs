using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quality : MonoBehaviour
{
    private bool res;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        int qualityLevel = QualitySettings.GetQualityLevel();
        Time.fixedDeltaTime = 0.007f;
        Application.targetFrameRate = 320;

        if (qualityLevel == 0)
        {
            QualitySettings.vSyncCount = 0;
            QualitySettings.antiAliasing = 0;
            res = true;
            
            if (res)
            {
                //Screen.SetResolution(1080, 1920, true);
            }
            
        }

        if (qualityLevel == 1)
        {
            QualitySettings.vSyncCount = 0;
            QualitySettings.antiAliasing = 0;
            res = true;
            
            if (res)
            {
                 //Screen.SetResolution(1080, 1920, true);
            }
        }

        if (qualityLevel == 2)
        {
            QualitySettings.vSyncCount = 1;
            QualitySettings.antiAliasing = 2;
            res = false;
        }
    }
    
}