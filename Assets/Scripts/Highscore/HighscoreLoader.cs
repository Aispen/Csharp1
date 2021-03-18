using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreLoader : MonoBehaviour
{
    public HighScoreManager manager;
    public TMP_Text scoreText;
    public TMP_Text whoPlayedText;


    void DisplayHighscore()
    {
        scoreText.text = manager.score.ToString();
        whoPlayedText.text = manager.whoPlayed;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.RandomizeStats();
            DisplayHighscore();
        }
    }
}
