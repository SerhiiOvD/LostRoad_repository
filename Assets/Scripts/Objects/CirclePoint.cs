using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePoint : MonoBehaviour
{
    public GameObject UIPoints;
    private Transform pos;
    
    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            Vector3 pos = new Vector3(transform.position.x + 1.5f,transform.position.y,transform.position.z);
            Instantiate(UIPoints,pos,transform.rotation);
            Score.scoreCount += 50;
        }
    }
}
