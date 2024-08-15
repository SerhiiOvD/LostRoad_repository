using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Score : MonoBehaviour
{

    public Text scoreText;
    public static float scoreCount; // Static field to hold the score
    public Ease animEase;
    private RectTransform transf;

    private float previousScoreCount; // To track the previous score

    void Awake()
    {
        transf = GetComponent<RectTransform>();
        previousScoreCount = scoreCount; // Initialize previous score to current score
    }

    void Start()
    {
        // Optionally start with an initial animation or setup
        UpdateScoreText(); // Initialize score text
    }

    void AnimateScore()
    {
        // Animate the RectTransform's Y position
        transf.DOMoveY(1060f, 0.1f)
              .SetEase(animEase)
              .SetLoops(2, LoopType.Yoyo);
    }

    void UpdateScoreText()
    {
        // Update the UI text with the rounded score
        scoreText.text = "Score : " + Mathf.Round(scoreCount);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the score has changed
        if (previousScoreCount != scoreCount)
        {
            // Trigger the animation and update the score text
            AnimateScore();
            UpdateScoreText();

            // Update previous score to the current score
            previousScoreCount = scoreCount;
        }
    }
    
}


