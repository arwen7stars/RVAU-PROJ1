using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHighscoreMessage : MonoBehaviour {
    // game object with highest score message
    public GameObject highscoreMsg;

    // time showing message
    private const float SHOWING_MSG = 2.0f;

    // time left to show message
    private float timeLeft = SHOWING_MSG;

    // check if current score is highest score yet
    public void CheckIfHighestScore(int score)
    {
        if (PlayerPrefs.HasKey("High Scores 0"))
        {
            int currHighest = PlayerPrefs.GetInt("High Scores 0");
            if (score > currHighest)
            {
                StartCoroutine(ShowScoreMsg());
            }
        }
    }

    // show score message
    IEnumerator ShowScoreMsg()
    {
        bool showingMsg = false;
        while (timeLeft > 0)
        {
            if (!showingMsg)
            {
                highscoreMsg.SetActive(true);
            }
            timeLeft -= Time.deltaTime;
        }
        highscoreMsg.SetActive(false);
        timeLeft = SHOWING_MSG;

        yield return null;
    }
}
