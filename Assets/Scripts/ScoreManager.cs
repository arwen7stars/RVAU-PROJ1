using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {

    int score = 0;
    public TextMeshProUGUI textScore;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void incrementScore()
    {
        score++;
        textScore.text = "Score: " + score;
    }
}
