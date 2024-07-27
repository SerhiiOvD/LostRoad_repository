using UnityEngine;
using System.Collections;

public class ZombieSpawn : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject effect;
    Vector3 zombiePosition;
    Quaternion zombieRotation;
    [SerializeField]float time = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //InvokeRepeating("spawnZombie", 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ZombieSpawningWithEffects(time));
            //spawnZombie();
        }
    }

    IEnumerator ZombieSpawningWithEffects(float time)
    {
        zombiePosition = new Vector3(3, 5, 6);
        zombieRotation = new Quaternion(5, 6, 7, 0);
        Instantiate(zombiePrefab, zombiePosition, zombieRotation);
        yield return new WaitForSeconds(time);
        Instantiate(effect,zombiePosition,zombieRotation); 
    }

    void spawnZombie()
    {
        zombiePosition = new Vector3(3, 5, 6);
        zombieRotation = new Quaternion(5, 6, 7, 0);
        Instantiate(zombiePrefab, zombiePosition, zombieRotation);
    }

    
}


