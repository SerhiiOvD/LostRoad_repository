using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FuelUI : MonoBehaviour
{
[HideInInspector]    public Slider slider;
    public Image fillBar;
    public Player fuelPoints;
   // RectTransform rt;
    
    


    // Start is called before the first frame update
    void Start()
    {
        
        slider = GetComponent<Slider>();//Get Slide component 
        
        /*Animation test*/
        //rt = GetComponent<RectTransform>();//Get Rect Transform component
        
        
        /*Vector2 startPosition = rt.anchoredPosition;//Perform start position as anchored position
        
        Vector2 leftPosition = new Vector2(-1024, startPosition.y);
        Vector2 rightPosition = new Vector2(-1026, startPosition.y);

        
        if (fuelPoints.currentlyFuel < 20)
        {
        rt.DOAnchorPos(leftPosition, 0.1f)
          .SetLoops(-1, LoopType.Yoyo);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //float fillValue = fs.currentlyFuel/fuelPoints.maxFuel;
        slider.value = fuelPoints.currentlyFuel;
        
        
        if(slider.value == 0){
            Destroy(fillBar);
        } 

    }
}
