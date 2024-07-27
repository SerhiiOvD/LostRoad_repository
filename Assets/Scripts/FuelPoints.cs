using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelPoints : MonoBehaviour
{

    public float maxFuel = 100f;
    [HideInInspector]
    public float currentlyFuel;
    private Rigidbody rd;
    public float decreasingSpeed = 50;
    public GameObject gameOver;
    public GameObject mobileButtons;

    // Start is called before the first frame update
    void Start()
    {
        currentlyFuel = maxFuel;// Perform maxFuel to currentlyFuel
        rd = GetComponent<Rigidbody>();// Get RigidBody component
        InvokeRepeating("FuelDecrease",0.2f,0.2f);// Invoke every 0.5 second void FuelDecreace
    }

    void FuelDecrease()
    {
        currentlyFuel -= decreasingSpeed * Time.deltaTime * 2;//Decrease "currentlyFuel" by "decreasing"
        if (currentlyFuel <= 0){//Check if fuel is <= 0;
            Die();//Invoke void Die
        }
    }

    void Die()
    {
        rd.isKinematic = true;//Is kinematic mark
        CancelInvoke("FuelDecrease");//Cancelling InvokeReapiting method
        gameOver.SetActive(true);
        mobileButtons.SetActive(false);
        Debug.Log("Fuel is out");
    }
}
