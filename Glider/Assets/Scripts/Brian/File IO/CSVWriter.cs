using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    [System.Serializable]
    public class HighScore
    {
        public string playerName;
        public float time;
        public float score;
    }
    [System.Serializable]
    public class HighScoreList
    {
        public HighScore[] highScore;
    }

    public HighScoreList highScoreList = new HighScoreList();

    void Start()
    {
        WritesCSV();
    }

    public void WritesCSV()
    {
        string filename = Application.dataPath + "/GliderScores.csv";

        if (highScoreList.highScore.Length > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Name, Time, Score");
            tw.Close();

            tw = new StreamWriter(filename, true);

            for (int i = 0; i < highScoreList.highScore.Length; i++)
            {
                tw.WriteLine(highScoreList.highScore[i].playerName + "," + highScoreList.highScore[i].time + "," + highScoreList.highScore[i].score);
            }
            tw.Close();
        }
    }
}
