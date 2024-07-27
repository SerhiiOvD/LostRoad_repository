using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent agent; // NavMeshAgent for navigation
    private bool isDie = false; // Flag to check if the zombie is dead
    private CharacterController characterController; // CharacterController for the zombie
    private float gravitySpeed = -9.81f; // Gravity speed

    private Collider coll; // Collider for the zombie
    private Animator animator; // Animator for the zombie
    private float verticalVelocity; // Vertical velocity for gravity calculations

    public GameObject ScorePointUI;

    void Start() 
    { 
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        characterController = GetComponent<CharacterController>(); // Get the CharacterController component
        animator = GetComponent<Animator>(); // Get the Animator component
        coll = GetComponent<Collider>(); // Get the Collider component

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

        if (characterController != null)
        { 
            ApplyGravity(); // Apply gravity to the zombie
        } 
    }

    void ApplyGravity()
    { 
        if(characterController.isGrounded)
        { 
            verticalVelocity = -1f; // Set vertical velocity to -1 if grounded
        } 
        else
        { 
            verticalVelocity += gravitySpeed * Time.deltaTime; // Apply gravity if not grounded
        } 
        Vector3 gravityMove = new Vector3(0, verticalVelocity, 0); // Create a Vector3 for gravity movement

        characterController.Move(gravityMove * Time.deltaTime); // Move the character controller with gravity
    }

    void OnTriggerEnter(Collider col) 
    { 
        if (col.gameObject.tag == "Player" && isDie == false) 
        { 
            Score.scoreCount += 10; // Increase the score
            isDie = true; // Set the zombie as dead
            Death(); // Call the Death function
            transform.Translate(0, 3 * Time.deltaTime, 0); // Move object on Y axis after collision

            Vector3 instantiatePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z); // Instantiate position for UI score
            Instantiate(ScorePointUI, instantiatePos, transform.rotation); // Instantiate UI Canvas
        } 
    }

    void Death() 
    { 
        Destroy(agent); // Destroy the NavMeshAgent component
        Destroy(coll); // Destroy the Collider component
        Destroy(animator); // Destroy the Animator component
        Destroy(gameObject, 5f); // Destroy the zombie game object after 5 seconds
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