using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoleMovement : MonoBehaviour
{

    public GameObject[] moles;                      // vector containing all mole's game objects
    public bool renderingStarted = false;           // rendering of the mole ground started
    public bool renderingStopped = false;           // target is no longer being tracked

    private bool[] upAndDown;                       // vector indicating whether mole is up or down (true : up & false : down)

    private bool[] countdownOver;                   // vector indicating whether countdown is over or not for each mole
    private bool[] countdownOngoing;                // vector indicating whether countdown is ongoing or not for each mole
    private float[] countdownTimes;                 // vector with countdown timers for each mole

    private int[] randomMoles;                      // random moles to hit on every TIME_BTW_RANDOMS seconds from the first wave of moles

    private const float WAITING_TIME = 1f;          // waiting time when the mole is up
    private const int MINIMUM = 0;                  // minimum number corresponding to moles' indexes to generate random numbers
    private const int MAXIMUM = 8;                  // maximum number corresponding to moles' indexes to generate random numbers

    private const float TIME_BTW_RANDOMS = 1f;      // time it takes to generate random moles
    private const int MAXIMUM_NO_MOLES = 5;         // maximum numbers of moles to appear simultaneously
    private const int MOLE_PROBABILITY = 25;        // probability to show a mole (TODO: increase over time)

    private bool coroutineStarted = false;

    // Use this for initialization
    void Start()
    {
        upAndDown = new bool[moles.Length];

        countdownOver = new bool[moles.Length];
        countdownOngoing = new bool[moles.Length];
        countdownTimes = new float[moles.Length];

        randomMoles = new int[MAXIMUM_NO_MOLES];

        for (int i = 0; i < moles.Length; i++)
        {
            upAndDown[i] = false;

            countdownOver[i] = false;
            countdownOngoing[i] = false;
            countdownTimes[i] = 0f;

            if (i < MAXIMUM_NO_MOLES)
            {
                randomMoles[i] = -1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (renderingStarted)
        {
            if (!coroutineStarted)
            {
                StartCoroutine(NumberGen());
                coroutineStarted = true;
            }

            for (int i = 0; i < moles.Length; i++)
            {

                if (upAndDown[i] && !countdownOver[i])                      // if mole is up and mole's countdown isn't over
                {
                    StartCountdown(i);
                }
                else if (upAndDown[i] && countdownOver[i])
                {
                    ShowMole(i, moles[i], false);
                }
            }
        }
    }

    void showRandomMoles()
    {
        int firstRandMole = randomMoles[0];

        if (!upAndDown[firstRandMole] && !countdownOngoing[firstRandMole])
        {
            ShowMole(firstRandMole, moles[firstRandMole], true);
        }

        System.Random rand = new System.Random();
        for (int i = 1; i < randomMoles.Length; i++)
        {
            if (rand.Next(1, 101) <= MOLE_PROBABILITY)
            {
                int randomIndex = randomMoles[i];

                if (!upAndDown[randomIndex] && !countdownOngoing[randomIndex])
                {
                    ShowMole(randomIndex, moles[randomIndex], true);
                }
            }
        }
    }

    void ShowMole(int moleIndex, GameObject mole, bool upOrDown)
    {
        mole.SetActive(upOrDown);
        upAndDown[moleIndex] = upOrDown;

        if (upOrDown)
            countdownOver[moleIndex] = false;

        return;
    }

    void StartCountdown(int moleIndex)
    {
        if (countdownTimes[moleIndex] < WAITING_TIME)
        {
            countdownTimes[moleIndex] += Time.deltaTime;

            if (!countdownOngoing[moleIndex])
            {
                countdownOngoing[moleIndex] = true;
            }
        }
        else
        {
            countdownOngoing[moleIndex] = false;
            countdownOver[moleIndex] = true;
            countdownTimes[moleIndex] = 0f;
        }

        return;
    }

    IEnumerator NumberGen()
    {
        while (true)
        {
        for (int i = 0; i < randomMoles.Length; i++)
            {
                randomMoles[i] = GetRandomNumber(i);
            }

            showRandomMoles();

            yield return new WaitForSeconds(TIME_BTW_RANDOMS);
            
        }
    }

    /**
     * Gets random moles' indexes except the ones that have already been selected
     * 
     * */
    private int GetRandomNumber(int moleIndex)
    {
        HashSet<int> exclude = new HashSet<int>();

        for (int i = 0; i < randomMoles.Length; i++)
        {
            if (randomMoles[i] != -1)
            {
                exclude.Add(randomMoles[i]);
            }
        }

        var range = Enumerable.Range(MINIMUM, MAXIMUM).Where(i => !exclude.Contains(i));

        var rand = new System.Random();
        int index = rand.Next(MINIMUM, MAXIMUM - exclude.Count);

        return range.ElementAt(index);
    }

    public bool GetRenderingStarted()
    {
        return renderingStarted;
    }

    public void SetRenderingStarted(bool value)
    {
        renderingStarted = value;
    }

    public bool GetRenderingStopped()
    {
        return renderingStopped;
    }

    public void SetRenderingStopped(bool value)
    {
        renderingStopped = value;
    }

}
