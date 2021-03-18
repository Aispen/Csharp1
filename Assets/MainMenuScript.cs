using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public static bool isPlayer;

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("PlayerGame");
        isPlayer = true;
    }
    public void AgentPlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("AgentGame");
        isPlayer = false;
    }
    public void GoToHighScoreMenu()
    {
        SceneManager.LoadScene("HighscoreMenu");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
