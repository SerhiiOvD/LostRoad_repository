using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    public GameObject zombiePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantZombies",3f,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantZombies()
    {
        Instantiate(zombiePrefab,transform.position,transform.rotation);
    }
}
