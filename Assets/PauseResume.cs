using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseResume : MonoBehaviour
{
    
    public void PauseResumeButton(){
        PauseUI.isPaused = !PauseUI.isPaused;
        Time.timeScale = 1;
    }
}
