//  Name: MainMenu.cs
//  Author: Connor Larsen
//  Purpose:

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text title;

    private string path = "Assets/Resources/titles.txt";
    string[] titles;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        titles = new string[TotalLines(path)];
        LoadTitles();
        int index = Random.Range(0, TotalLines(path));
        title.text = "CYBERTRUCK VS TRAIN: \n" + titles[index];
    }

    public void StartGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadTitles()
    {
        if (System.IO.File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);

            for (int i = 0; i < TotalLines(path); i++)
            {
                titles[i] = reader.ReadLine();
            }

            reader.Close();
        }
    }

    private int TotalLines(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            int i = 0;
            while (reader.ReadLine() != null) { i++; }
            return i;
        }
    }
}