using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float points = 10;//+ points
    public FuelPoints fuelPoints;//Reference for script
    public GameObject fuelPointUI;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,1,0);//Rotate object every frame on Y axis;
        
        
    }
    void OnTriggerEnter(Collider coll){
        if (coll.gameObject.CompareTag("Player")){//Codition which check collision with tag "Player"
            
            
            Destroy(gameObject);//Selfdestroy
            fuelPoints.currentlyFuel += points; // Append fuel on certain points
            
            Vector3 instantiatePosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z); // Instantiate position for UI score
            Instantiate(fuelPointUI, instantiatePosition, transform.rotation); // Instantiate UI Canvas

        }
    }
}
