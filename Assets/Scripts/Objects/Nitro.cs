using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Nitro : MonoBehaviour
{
    public GameObject nitroEffect;//Nitro Effect
    public GameObject spawnNitro;//Spawn Point of nitro effect
    public static float OnNitro = 0;//Nitro variable
    public static float forceNitroAmount = 50;//Acceleration of car
    public Player playerScript;//Ref on Player Script 
    public AudioClip soundClip;
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")){
            OnNitro ++;
            //nitroEffect.SetActive(true);
            GameObject newObject = Instantiate(nitroEffect,spawnNitro.transform.position,spawnNitro.transform.rotation);//Instantiation Nitro GameObject in SpawnPosition
            newObject.transform.parent = spawnNitro.transform;//Attaching instantiated object on car
            playerScript.playerSource.PlayOneShot(soundClip);
            Destroy(gameObject);//Destroy this object
        }
    }
    
    void Update(){
        transform.Rotate(0,0,1);//Object rotation on axis z + 1 every frame (for animation)
    }
    
}
