using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using DG.Tweening;

public class Enemy : MonoBehaviour
{

   private NavMeshAgent agent; // NavMeshAgent for navigation
    private bool isDie = false; // Flag to check if the enemy is dead

    private Collider coll; // Collider for the enemy
    private Animator animator; // Animator for the enemy
    public GameObject ScorePointUI;
    private Transform cam;
    private AudioSource aSource;
    public AudioClip soundEffect;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        animator = GetComponent<Animator>(); // Get the Animator component
        coll = GetComponent<Collider>(); // Get the Collider component
        cam = Camera.main.transform; // Get the main camera transform
        aSource = GetComponent<AudioSource>();//Get the AudioSource component
    }

    void Start() 
    { 
        MoveToRandomPoint(); // Move to a random point
    }

    void Update() 
    { 
        if(agent != null) 
        { 
            // Check if the agent is not pending and has reached the current destination
            if (!agent.pathPending && agent.remainingDistance < 0.5f) 
            { 
                MoveToRandomPoint(); // Move to a random point
            } 
        } 
    }

    void OnTriggerEnter(Collider col) 
    { 
        if (col.gameObject.tag == "Player" && isDie == false) 
        { 
            cam.DOShakePosition(0.1f, 1, 3, 90, false, true);

            Score.scoreCount += 10; // Increase the score
            isDie = true; // Set the enemy as dead
            Death(); // Call the Death function
            transform.Translate(0, 3 * Time.deltaTime, 0); // Move object on Y axis after collision

            Vector3 instantiatePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z); // Instantiate position for UI score
            Instantiate(ScorePointUI, instantiatePos, transform.rotation); // Instantiate UI Canvas
        } 
    }

    void Death() 
    { 
        aSource.PlayOneShot(soundEffect); // Play sound effect 
        Destroy(agent); // Destroy the NavMeshAgent component
        Destroy(coll); // Destroy the Collider component
        Destroy(animator); // Destroy the Animator component
        Destroy(gameObject, 5f);// Destroy Game Object
    }

    void MoveToRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10f; // Random direction inside a sphere with radius 10 units
        randomDirection += transform.position; // Add the random direction to the current position

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10f, 1)) 
        {
            agent.destination = hit.position; // Set the agent's destination to the random point
        }
    }
}