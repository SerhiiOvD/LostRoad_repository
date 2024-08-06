using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPause : MonoBehaviour
{
    private AudioSource audioS;

    void Start(){
        audioS = GetComponent<AudioSource>();
    }
    void Update(){ 
        if (PauseUI.isPaused == true)//IF game is paused 
        {
            audioS.mute = true;//Audio Sorce is muted
        }
        else if (PauseUI.isPaused == false)
        {
            audioS.mute = false;
        }
    }
}
