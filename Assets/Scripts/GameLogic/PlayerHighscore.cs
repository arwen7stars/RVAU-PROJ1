using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHighscore : MonoBehaviour {

    // Highscores' list max size
    private const int SCORES_SIZE = 5;

    // Highscores' list
    private static List<int> highScores = new List<int>();

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Add score to highscores' list (if it's high enough)
    public static void AddScore(int score)
    {
        highScores.Add(score);
        highScores.Sort();
        highScores.Reverse();

        StoreScores();
    }

    // Store score on player prefs
    public static void StoreScores()
    {
        int listSize = SCORES_SIZE;
        if (highScores.Count <= SCORES_SIZE)
        {
            listSize = highScores.Count;
        }

        for (int i = 0; i < listSize; i++) {
            PlayerPrefs.SetInt("High Scores " + i, highScores[i]);
        }
    }
}