using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    [SerializeField] TextAsset textAssetData;
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
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 3 - 1;
        highScoreList.highScore = new HighScore[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            highScoreList.highScore[i] = new HighScore();
            highScoreList.highScore[i].playerName = data[3 * (i + 1)];
            highScoreList.highScore[i].time = float.Parse(data[3 * (i + 1) + 1]);
            highScoreList.highScore[i].score = float.Parse(data[3 * (i + 1) + 2]);
        }
    }
}
