using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOverTweening : MonoBehaviour
{
    private RectTransform go;
    public Ease typeAnimation;
    // Start is called before the first frame update

    void Awake(){
        go = GetComponent<RectTransform>();
    }
    void Start()
    {
        go.DOMoveY(700f, 2f)
          .SetEase(typeAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
