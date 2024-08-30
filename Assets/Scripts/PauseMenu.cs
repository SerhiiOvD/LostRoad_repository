using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausedMenu;

    void Awake()
    {
        RectTransform rTransform = GetComponent<RectTransform>();
    }
    
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (PauseUI.isPaused){
            PausedMenu.SetActive(true);
        }
        else{
            PausedMenu.SetActive(false);
        }
    }
}
