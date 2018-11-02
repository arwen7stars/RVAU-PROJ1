using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public GameObject menu;

    public GameObject menuDefault;
    public GameObject menuDifficulty;

    public Button normalBtn;
    public Button hardBtn;

    private bool stopGame = false;

    public void OpenMenu()
    {
        menu.SetActive(true);
        menuDefault.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        menu.SetActive(false);
        menuDefault.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        menu.SetActive(false);
        menuDefault.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Difficulty()
    {
        menuDefault.SetActive(false);
        menuDifficulty.SetActive(true);

        if (StaticSettings.difficulty == StaticSettings.NORMAL)
        {
            normalBtn.interactable = false;
            hardBtn.interactable = true;
        } else {
            normalBtn.interactable = true;
            hardBtn.interactable = false;
        }
    }

    public void NormalDifficulty()
    {
        StaticSettings.changeDifficulty(StaticSettings.NORMAL);

        normalBtn.interactable = false;
        hardBtn.interactable = true;
    }

    public void HardDifficulty()
    {
        StaticSettings.changeDifficulty(StaticSettings.HARD);

        normalBtn.interactable = true;
        hardBtn.interactable = false;
    }

    public void BackToMenu()
    {
        menuDefault.SetActive(true);
        menuDifficulty.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public bool GetStopGame()
    {
        return stopGame;
    }

}
