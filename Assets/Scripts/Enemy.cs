using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using DG.Tweening;

public class Enemy : MonoBehaviour
{

   private NavMeshAgent agent; // NavMeshAgent component for navigating the enemy
    private bool isDie = false; // Flag to check if the enemy is dead

    private Collider coll; // Collider component for the enemy
    private Animator animator; // Animator component for the enemy
    public GameObject ScorePointUI; // UI element to display score when the enemy is defeated
    private Transform cam; // Reference to the main camera
    private AudioSource aSource; // AudioSource component to play sound effects
    public AudioClip soundEffect; // Sound effect to play upon enemy death

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        animator = GetComponent<Animator>(); // Get the Animator component
        coll = GetComponent<Collider>(); // Get the Collider component
        cam = Camera.main.transform; // Get the main camera transform
        aSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    void Start() 
    { 
        MoveToRandomPoint(); // Move to a random point when the game starts
    }

    void Update() 
    { 
        if(agent != null && !isDie) 
        { 
            // Check if the agent has reached the current destination
            if (!agent.pathPending && agent.remainingDistance < 0.5f) 
            { 
                MoveToRandomPoint(); // Move to a new random point
            } 
        } 
    }

    void OnTriggerEnter(Collider col) 
    { 
        // Check if the enemy collides with the player and is not already dead
        if (col.gameObject.tag == "Player" && !isDie) 
        { 
            cam.DOShakePosition(0.1f, 1, 3, 90, false, true); // Shake the camera upon collision

            Score.scoreCount += 10; // Increase the score by 10
            isDie = true; // Mark the enemy as dead
            Death(); // Trigger the death sequence
            transform.Translate(0, 3 * Time.deltaTime, 0); // Slightly move the enemy upwards after collision

            // Instantiate the score UI at the enemy's position
            Vector3 instantiatePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            Instantiate(ScorePointUI, instantiatePos, transform.rotation);
        } 
    }

    void Death() 
    { 
        aSource.PlayOneShot(soundEffect); // Play the death sound effect
        Destroy(agent); // Destroy the NavMeshAgent component to stop movement
        Destroy(coll); // Destroy the Collider component to prevent further collisions
        Destroy(animator); // Destroy the Animator component
        Destroy(gameObject, 5f); // Destroy the enemy game object after 5 seconds
    }

    void MoveToRandomPoint()
    {
        // Generate a random direction within a sphere of radius 10 units
        Vector3 randomDirection = Random.insideUnitSphere * 10f;
        randomDirection += transform.position; // Offset by the current position

        NavMeshHit hit;
        // Sample a valid position on the NavMesh within 10 units
        if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas)) 
        {
            agent.SetDestination(hit.position); // Set the agent's destination to the random point
        }
        else
        {
            Debug.LogWarning("Could not find a valid position on NavMesh"); // Log a warning if no valid position is found
        }
    }
}