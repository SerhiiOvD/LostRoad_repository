using UnityEngine;

public class Tags : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (this.gameObject.tag == "Enemy")
        {
            Debug.Log("Boba");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
