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
    //TMP_Text endScoreTxt;
    void Start()
    {
        //endScoreTxt = GameObject.Find("Canvas/EndScoreText").GetComponent<TMP_Text>();
        //endScoreTxt = GetComponent<Text>();
    }
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
        //endScoreTxt.text = ScoreScript.scoreValue.ToString();
        EndGameScore.endScore = ScoreScript.scoreValue;
        Time.timeScale = 0;
    }
    public void Restart()
    {
        GameEndUI.SetActive(false);
        Time.timeScale = 1;
        TimerScript.timeToStop = false; TimerScript.timeValue = 20.00f; ScoreScript.scoreValue = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SpawnScript.ResetSettings();
    }
    public void MainMenu()
    {
        GameEndUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
