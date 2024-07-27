using UnityEngine;

public class MovementZombieB : MonoBehaviour
{
    float speed = 5f;
    Vector3 temp;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        temp = transform.localScale;

        temp.x += 5 * Time.deltaTime * speed;

        transform.localScale = temp;
    }
}
