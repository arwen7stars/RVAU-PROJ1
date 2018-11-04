using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    // find hammer and platform message
    public GameObject findGame;

    // game start message
    public GameObject gameStartMsg;

    // game platform target
    public TrackableHandler platform;

    // hammer target
    public TrackableHandler hammer;

    // time until game start
    private const float START_DELAY = 2.0f;

    // if game started
    private bool gameStart = false;

    // if message is being shown
    private bool messageShown = false;

    void Update()
    {
        if (!platform.GetFound() || !hammer.GetFound())
        {
            if (!findGame.activeSelf)
            {
                findGame.SetActive(true);               // message to find game platform and hammer
            }
        } else
        {
            if (!messageShown)
            {
                GetComponent<AudioSource>().Play();     // play main theme!

                messageShown = true;
                findGame.SetActive(false);

                StartCoroutine(StartGame());
            }
        }

    }

    // show timer over message
    IEnumerator StartGame()
    {

        gameStartMsg.SetActive(true);

        yield return new WaitForSeconds(START_DELAY);

        gameStart = true;


        gameStartMsg.SetActive(false);
    }

    public bool GetGameStart()
    {
        return gameStart;
    }
}
