using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float maxFuel = 100f;//Max count what fuel can store
    [HideInInspector]
    public float currentlyFuel;
    private Rigidbody rd;
    public float decreasingSpeed = 50;//Speed decreasing of fuel
    public GameObject gameOver;//Game Over GUI
    public GameObject mobileButtons;//Mobile buttons for UI
    [HideInInspector]
    public AudioSource playerSource;
    public AudioClip SFXCollision;

    // Start is called before the first frame update
    void Start()
    {
        currentlyFuel = maxFuel;// Perform maxFuel to currentlyFuel
        rd = GetComponent<Rigidbody>();
        playerSource = GetComponent<AudioSource>();// Get RigidBody component
    }

    
    void Update()
    {
        if (currentlyFuel > maxFuel){
            currentlyFuel = maxFuel;
        }


        currentlyFuel -= decreasingSpeed * Time.deltaTime ;//Decrease "currentlyFuel" by "decreasing"
        if (currentlyFuel <= 0){//Check if fuel is <= 0;
            Die();//Call Die function
        }
    }

    public void Die()
    {
        rd.isKinematic = true;//Is kinematic mark
        
        gameOver.SetActive(true);
        mobileButtons.SetActive(false);
        //Debug.Log("Fuel is out");
    }

    void OnCollisionEnter(Collision coll){
        if (coll.gameObject.CompareTag("Building")){
            playerSource.PlayOneShot(SFXCollision);
        }
    }
}