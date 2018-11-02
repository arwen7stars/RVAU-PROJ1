using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSettings : MonoBehaviour {
    public const float NORMAL_UPTIME = 2000f;
    public const float HARD_UPTIME = 1500f;

    public const string NORMAL = "normal";
    public const string HARD = "hard";

    public static string difficulty = "normal";       // normal or hard

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static void changeDifficulty(string diff)
    {
        difficulty = diff;
    }

    public static float setUptime()
    {
        if (difficulty == NORMAL)
        {
            return NORMAL_UPTIME;
        }
        else
        {
            return HARD_UPTIME;
        }
    }
}
