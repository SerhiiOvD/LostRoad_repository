using UnityEngine;

public class ZombieController : MonoBehaviour
{

    float speed = 5f;
    Vector3 currentlyPos;
    Animator animator;
    Rigidbody rb;
    float x;
    CharacterController characterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("checkPos", 5f, 5f);
        animator = GetComponent<Animator>();
        //rb.linearVelocity = new Vector3 (2,0,0);
        //animator.applyRootMotion = true;
    }

    // Update is called once per frame
    void Update()
    {

        x = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotateZ = Input.GetAxis("Horizontal") * speed;

        transform.Translate(0, 0, x);
        
        animator.SetFloat("Horizontal",x);
    
        //float rotateY = Input.GetAxis("Horizontal");
        transform.Rotate(0, rotateZ,0);

        if (Input.GetButton("Jump"))
        {
            transform.Translate(0,2 * Time.deltaTime,0);
            animator.SetTrigger("Jump");
        }
    }

    void checkPos()
    {
        currentlyPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Debug.Log(currentlyPos);
    }
}


