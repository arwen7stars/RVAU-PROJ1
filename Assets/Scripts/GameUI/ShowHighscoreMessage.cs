﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHighscoreMessage : MonoBehaviour {
    // game object with highest score message
    public GameObject highscoreMsg;

    // if has already shown highest score message on this match
    private bool messageShown = false;

    // time showing message
    private const float SHOWING_MSG = 2.0f;

    // first score key
    private const string FIRST_SCORE = "highscore0";

    // check if current score is highest score yet
    public void CheckIfHighestScore(int score)
    {
        if (!messageShown)
        {
            if (PlayerPrefs.HasKey(FIRST_SCORE))
            {
                int currHighest = PlayerPrefs.GetInt(FIRST_SCORE);
                if (score > currHighest)
                {
                    StartCoroutine(ShowScoreMsg());
                }
            }
        }
    }

    // show score message
    IEnumerator ShowScoreMsg()
    {
        highscoreMsg.SetActive(true);
        messageShown = true;

        yield return new WaitForSeconds(SHOWING_MSG);

        highscoreMsg.SetActive(false);
    }
}
