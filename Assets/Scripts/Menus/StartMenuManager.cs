using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

    // default menu
    public GameObject defaultMenu;

    // high scores screen
    public GameObject highScoresMenu;

    // get high scores
    public HighScores highScores;

    // difficultymenu
    public GameObject difficultyMenu;

    // button corresponding to normal difficulty
    public Button normalBtn;

    // button corresponding to hard difficulty
    public Button hardBtn;

    // int corresponding to level scene
    private const int LEVEL_SCENE = 1;

    public void PlayGame()
    {
        SceneManager.LoadScene(LEVEL_SCENE);
    }

    // show difficulty options
    public void Difficulty()
    {
        defaultMenu.SetActive(false);
        difficultyMenu.SetActive(true);

        if (StaticSettings.difficulty == StaticSettings.NORMAL)
        {
            normalBtn.interactable = false;
            hardBtn.interactable = true;
        }
        else
        {
            normalBtn.interactable = true;
            hardBtn.interactable = false;
        }
    }

    // player chose normal difficulty
    public void NormalDifficulty()
    {
        StaticSettings.changeDifficulty(StaticSettings.NORMAL);

        normalBtn.interactable = false;
        hardBtn.interactable = true;
    }

    // player chose hard difficulty
    public void HardDifficulty()
    {
        StaticSettings.changeDifficulty(StaticSettings.HARD);

        normalBtn.interactable = true;
        hardBtn.interactable = false;
    }

    // back to main menu from difficulty settings
    public void BackToMenuFromDifficulty()
    {
        defaultMenu.SetActive(true);
        difficultyMenu.SetActive(false);
    }

    // show high scores
    public void HighScores()
    {
        defaultMenu.SetActive(false);
        highScoresMenu.SetActive(true);

        highScores.ShowScores();
    }

    // back to main menu from high scores
    public void BackToMenuFromHighscores()
    {
        defaultMenu.SetActive(true);
        highScoresMenu.SetActive(false);
    }

    // exit game
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
