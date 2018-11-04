using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    
    // menu object with overlay
    public GameObject menu;

    // main menu options (continue, restart, difficulty, exit)
    public GameObject menuDefault;

    // difficulty options (normal, hard, back)
    public GameObject menuDifficulty;

    // button corresponding to normal difficulty
    public Button normalBtn;

    // button corresponding to hard difficulty
    public Button hardBtn;

    // digletts on the scene
    public Diglet[] digletts;

    // game is stopped or not due to menu
    private bool stopGame = false;

    // int corresponding to start menu scene
    private const int START_MENU_SCENE = 0;

    // open main menu
    public void OpenMenu()
    {
        menu.SetActive(true);
        menuDefault.SetActive(true);

        stopGame = true;
        Time.timeScale = 0;
    }

    // continue game
    public void Continue()
    {
        menu.SetActive(false);
        menuDefault.SetActive(false);

        stopGame = false;
        Time.timeScale = 1;
    }

    // restart game
    public void RestartGame()
    {
        menu.SetActive(false);
        menuDefault.SetActive(false);

        stopGame = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // show difficulty options
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

    // player chose normal difficulty
    public void NormalDifficulty()
    {
        StaticSettings.changeDifficulty(StaticSettings.NORMAL);

        normalBtn.interactable = false;
        hardBtn.interactable = true;

        for (int i = 0; i < digletts.Length; i++)
        {
            float tmpUptime = StaticSettings.setUptime();
            digletts[i].SetUptime(tmpUptime);
        }
    }

    // player chose hard difficulty
    public void HardDifficulty()
    {
        StaticSettings.changeDifficulty(StaticSettings.HARD);

        normalBtn.interactable = true;
        hardBtn.interactable = false;

        for (int i = 0; i < digletts.Length; i++)
        {
            float tmpUptime = StaticSettings.setUptime();
            digletts[i].SetUptime(tmpUptime);
        }
    }

    // back to main menu
    public void BackToMenu()
    {
        menuDefault.SetActive(true);
        menuDifficulty.SetActive(false);
    }

    // exit game
    public void ExitGame()
    {
        stopGame = false;
        Time.timeScale = 1;

        SceneManager.LoadScene(START_MENU_SCENE);
    }

    // checks if game stopped due to menu being shown or not
    public bool GetStopGame()
    {
        return stopGame;
    }

}
