using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour {

    // timer in seconds
    public const int TIMER_SECONDS = 60;

    // timer text UI
    public TextMeshProUGUI textTimer;

    // if the game platform isn't being shown, not update timer
    public TrackableHandler platform;

    // if the hammer isn't being shown, not update timer
    public TrackableHandler hammer;

    // if menu is being shown, not update timer
    public MenuManager menu;

    // if game hasn't started, not update timer
    public Tutorial gameStart;

    // to get score after timer runs out
    public ScoreManager score;

    // how much time left in the timer
    private int timeLeft;

    // timer aux
    private float tmpTime;

    // minutes in string
    private string minutes;

    // seconds in string
    private string seconds;

    // game object with timer over message
    public GameObject timerOverMsg;

    // time showing message
    private const float SHOWING_MSG = 3.0f;

    // if score has been saved or not
    private bool savedScore = false;

    // int corresponding to start menu scene
    private const int START_MENU_SCENE = 0;

    // check if game over
    private bool gameOver = false;

    // Use this for initialization
    void Start () {
        timeLeft = TIMER_SECONDS;
        tmpTime = TIMER_SECONDS;
	}
	
	// Update is called once per frame
	void Update () {
        // if game hasn't started or if game is over, ignore
        if (!gameStart.GetGameStart() || gameOver) return;

        if (!platform.GetRendering() || !hammer.GetRendering() || menu.GetStopGame()) return;

        if (timeLeft <= 0)                                     // game ends if timer is over
        {
            if (!savedScore)
            {
                int finalScore = score.GetScore();             // get final score on this match
                PlayerHighscore.AddScore(finalScore);          // update high scores' table

                savedScore = true;
                gameOver = true;
                StartCoroutine(ShowTimerOverMsg());
            }
        } else
        {
            convertTime();
            textTimer.text = minutes + ":" + seconds;
        }
    }

    // show timer over message
    IEnumerator ShowTimerOverMsg()
    {

        timerOverMsg.SetActive(true);

        yield return new WaitForSeconds(SHOWING_MSG);

        timerOverMsg.SetActive(false);

        SceneManager.LoadScene(START_MENU_SCENE);
    }

    // Convert timeLeft to minutes and seconds
    private void convertTime()
    {
        tmpTime -= Time.deltaTime;
        timeLeft = (int)tmpTime;

        int minutesInt = timeLeft / 60;
        int secondsInt = timeLeft - minutesInt * 60;

        if (minutesInt < 10) { minutes = "0" + minutesInt; }
        else { minutes = minutesInt.ToString(); }

        if (secondsInt < 10) { seconds = "0" + secondsInt; }
        else { seconds = secondsInt.ToString(); }
    }

    public bool GetGameOver()
    {
        return gameOver;
    }
}
