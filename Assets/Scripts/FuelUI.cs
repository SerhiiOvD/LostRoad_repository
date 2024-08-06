using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    public Slider slider;
    public Image fillBar;
    public Player fuelPoints;

    // Start is called before the first frame update
    void Start()
    {
        
        slider = GetComponent<Slider>();

        //slider.value = fuelPoints.currentlyFuel;
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
