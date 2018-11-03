using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour {
    public GroundTrackableHandler trackableGame;
    public MenuManager menu;
    public TextMeshProUGUI textTimer;
    public const int TIMER_SECONDS = 60;

    private int timeLeft;
    private float tmpTime;
    private string minutes;
    private string seconds;

    // Use this for initialization
    void Start () {
        timeLeft = TIMER_SECONDS;
        tmpTime = TIMER_SECONDS;
	}
	
	// Update is called once per frame
	void Update () {

        if (!trackableGame.GetRenderingStarted() || trackableGame.GetRenderingStopped() || menu.GetStopGame())
        {
            return;
        }

        if (timeLeft <= 0)
        {
            Debug.Log("finished");
        } else
        {
            convertTime();
            textTimer.text = minutes + ":" + seconds;
        }
    }

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
}
