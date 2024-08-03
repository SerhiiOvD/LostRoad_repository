using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Pause(){
        
        isPaused = !isPaused;

        if (isPaused){
            Time.timeScale = 0;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }
}
