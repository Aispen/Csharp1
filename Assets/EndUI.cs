using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndUI : MonoBehaviour
{
    public static bool GameEnd = false;

    public GameObject GameEndUI;

    void Update()
    {
        if (TimerScript.timeToStop)
        {
            GameEnded();
        }

    }

    public void GameEnded()
    {
        GameEndUI.SetActive(true);
        EndGameScore.endScore = ScoreScript.scoreValue;
        Time.timeScale = 0;
    }
    public void Restart()
    {
        GameEndUI.SetActive(false);
        Time.timeScale = 1;
        ResetGame();
        if (MainMenuScript.isPlayer) { SceneManager.LoadScene("PlayerGame"); }
        else { SceneManager.LoadScene("AgentGame"); }

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        GameEndUI.SetActive(false);
        ResetGame();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    void ResetGame()
    {
        TimerScript.timeToStop = false; TimerScript.timeValue = 20f; ScoreScript.scoreValue = 0;
        SpawnScript.ResetCollectibles();
    }
}
