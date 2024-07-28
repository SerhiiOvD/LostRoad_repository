using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowOutlineUI : MonoBehaviour
{

    public Color startColor = Color.red;
    public Color endColor = Color.blue;

    public float speedAnimation = 1f;
    private Outline line;//Component variable


    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<Outline>();// Get component "OutLine"
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time * speedAnimation, 1f);
        line.effectColor = Color.Lerp(startColor, endColor, t);
    }
}
