using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[ExecuteInEditMode]
public class SKYPRO_Lighting : MonoBehaviour
{
    Light _light;
    [SerializeField] private float sunIntensity = 3;
    //[SerializeField] private Gradient dayToEveningGradient;
    [SerializeField] private Color dayColour;
    [SerializeField] private Color eveningColour;


    void Update()
    {
        if(_light == null)
        {
            _light = GetComponent<Light>();
        }

        float dotProduct = Vector3.Dot(-transform.forward, Vector3.up);
        float clampedDot = Mathf.Clamp((dotProduct + 0.9f), 0, 1);
        float topDot = (1 - Mathf.Clamp01(dotProduct)) * Mathf.Clamp01(Mathf.Sign(dotProduct));
        float bottomDot = (1 - Mathf.Clamp01(-dotProduct)) * Mathf.Clamp01(Mathf.Sign(-dotProduct));
        topDot = math.smoothstep(0.25f, 0.4f, topDot);
        bottomDot = Mathf.Pow(bottomDot, 5);

        _light.intensity = Mathf.Lerp(0.1f, sunIntensity, Mathf.Pow(clampedDot, 5));
        _light.color = Color.Lerp(dayColour, eveningColour, topDot + bottomDot);

        if(transform.localEulerAngles.x == -90)
        {
            transform.localEulerAngles = new Vector3(-89.9f, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
