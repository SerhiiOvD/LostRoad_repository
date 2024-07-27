using UnityEngine;

public class Arrays : MonoBehaviour
{

        public string[] arra;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
         

    void Start()
    {
        arra = new string[3]; 
        
        arra[0] = "Aboba";
        arra[1] = "Boba";
        arra[2] = "Biba";

        foreach(string i in arra ){
            Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
