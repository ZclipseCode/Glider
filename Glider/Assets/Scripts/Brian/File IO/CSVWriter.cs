using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    public void WritesCSV(string playerName, float time, float score)
    {
        string filename = Application.dataPath + "/GliderScores.csv";

        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("Name, Time, Score");
        tw.Close();

        tw = new StreamWriter(filename, true);

        tw.WriteLine(playerName + "," + time + "," + score);
        tw.Close();
    }
}
