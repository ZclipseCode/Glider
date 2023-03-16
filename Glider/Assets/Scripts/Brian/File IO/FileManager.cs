using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Forms;

public class FileManager : MonoBehaviour
{
    //void Start()
    //{
    //    SelectFile();
    //}

    public void SelectFile(CSVReader reader)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "CSV files (*.csv)|*.csv";
        openFileDialog.InitialDirectory = "C:\\";
        openFileDialog.Title = "Select a file";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFileDialog.FileName;
            // Do something with the selected file path
            //Debug.Log("Selected file: " + filePath);
            reader.ReadCSV(filePath);
        }
    }
}
