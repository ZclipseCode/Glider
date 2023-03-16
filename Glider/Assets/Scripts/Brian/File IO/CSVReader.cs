using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class CSVReader : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    public class HighScore
    {
        public string playerName;
        public float time;
        public float score;

        public HighScore(string playerName, float time, float score)
        {
            this.playerName = playerName;
            this.time = time;
            this.score = score;
        }
    }
 
    public class HighScoreList
    {
        public HighScore[] highScore;
    }
    public HighScoreList highScoreList = new HighScoreList();

    public void ReadCSV(string path)
    {
        if (File.Exists(path))
        {
            int count = 0;
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    reader.ReadLine();
                    count++;
                }
            }

            highScoreList.highScore = new HighScore[count - 1];

            using (StreamReader reader = new StreamReader(path))
            {
                reader.ReadLine();

                for (int i = 0; i < highScoreList.highScore.Length; i++)
                {
                    string row = reader.ReadLine();
                    string[] columns = row.Split(',');

                    string playerName = columns[0];
                    float time = float.Parse(columns[1]);
                    float score = float.Parse(columns[2]);

                    highScoreList.highScore[i] = new HighScore(playerName, time, score);
                    scoreText = GameObject.FindGameObjectWithTag("Scores Layout").GetComponentInChildren<TextMeshProUGUI>();
                    DisplayScores(highScoreList.highScore[i]);
                }
            }
        }
    }

    public void DisplayScores(HighScore highScore)
    {
        scoreText.text += $"{highScore.playerName} {highScore.time} {highScore.score}";
    }
}
