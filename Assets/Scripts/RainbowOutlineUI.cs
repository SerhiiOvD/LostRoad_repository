using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RainbowOutlineUI : MonoBehaviour
{
    public float speedAnimation = 1f; // Duration of each color transition
    private Outline line; // Variable to hold the Outline component

    void Awake()
    {
        // Get the Outline component attached to the GameObject
        line = GetComponent<Outline>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Start the color animation
        Animate();
    }

    void Animate()
    {
        // Animate the Outline color to a random color over the specified duration
        line.DOColor(RandomColor(), speedAnimation)
            .OnComplete(Animate); // Recursively call Animate after the current animation is complete
    }

    // Generate a random color with full opacity (alpha = 1)
    Color RandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
    }
}