using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "New Highscore Entry", menuName = "Highscore entry")]
public class HighScoreManager : ScriptableObject
{
    public int score;
    public string whoPlayed;

    List<HighScoreEntry> HighscoreEntryList = new List<HighScoreEntry>()
        {
            new HighScoreEntry( 80,  1),
            new HighScoreEntry( 50,  0),
            new HighScoreEntry( 100,  0),
            new HighScoreEntry( 30,  1),
            new HighScoreEntry( 150,  1),
        };

    public void RandomizeStats()
    {
        score = Random.Range(10, 200);
        int rnd = Random.Range(1, 3);
        if (rnd == 1)
        {
            whoPlayed = "Player";
        }
        else
        {
            whoPlayed = "Agent";
        }
    }

    public void GetHighscoreFromList(int position)
    {
        CreateHighscoreEntry(HighscoreEntryList[position]);
    }

    public void CreateHighscoreEntry(HighScoreEntry highScoreEntry)
    {
        score = highScoreEntry.entryScore;
        if(highScoreEntry.entryWhoPlayed == 0)
        {
            whoPlayed = "Player";
        }
        else
        {
            whoPlayed = "Agent";
        }
    }
    public class HighScoreEntry
    {
        public int entryScore;
        public int entryWhoPlayed; //0 - player //1 - agent

        public HighScoreEntry(int entryScore, int entryWhoPlayed)
        {
            this.entryScore = entryScore;
            this.entryWhoPlayed = entryWhoPlayed;
        }
    }
}

