using UnityEngine;

public class StaticSettings : MonoBehaviour {
    // diglett's uptime in normal difficulty
    public const float NORMAL_UPTIME = 2000f;

    // diglett's uptime in hard difficulty
    public const float HARD_UPTIME = 1500f;

    // normal difficulty
    public const string NORMAL = "normal";

    // hard difficulty
    public const string HARD = "hard";

    // default difficulty is normal
    public static string difficulty = "normal";

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Changes game's difficulty
    public static void changeDifficulty(string diff)
    {
        difficulty = diff;
    }

    // Set diglett's uptime depending on difficulty
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
