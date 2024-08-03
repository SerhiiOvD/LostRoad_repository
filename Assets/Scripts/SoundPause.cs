using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPause : MonoBehaviour
{
    private AudioSource audio;

    void Start(){
        audio = GetComponent<AudioSource>();
    }
    void Update(){ 
        if (PauseUI.isPaused == true)//IF game is paused 
        {
            audio.mute = true;//Audio Sorce is muted
        }
        else if (PauseUI.isPaused == false)
        {
            audio.mute = false;
        }
    }
}
