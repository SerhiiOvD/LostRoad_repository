using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour
{
    Text scoreT;// Component Text
    [SerializeField]
    public Score scriptScore;// referenc on "Score" script
    // Start is called before the first frame update
    
    void Awake(){
        scoreT = GetComponent<Text>();//Getting Text component 
    }
    void Start()
    {
        if (scoreT){
            scoreT.text = scriptScore.previousScoreCount.ToString();// Show your total collected score
        }
    }

   
}
