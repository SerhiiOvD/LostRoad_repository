using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPointsUI : MonoBehaviour
{
   // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1);
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;

        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

        transform.Translate(0,1 * Time.deltaTime,0);
    }
}
