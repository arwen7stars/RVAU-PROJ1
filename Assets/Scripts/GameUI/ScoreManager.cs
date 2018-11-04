using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    // score text UI
    public TextMeshProUGUI textScore;

    // if current score is highest, show message
    public ShowHighscoreMessage highScoreMsg;

    // current score
    private int score = 0;

    // increment current score
    public void incrementScore()
    {
        score++;
        highScoreMsg.CheckIfHighestScore(score);        // checks if current score is highest
        textScore.text = "Score: " + score;
    }

    // get current score
    public int GetScore()
    {
        return score;
    }
}
