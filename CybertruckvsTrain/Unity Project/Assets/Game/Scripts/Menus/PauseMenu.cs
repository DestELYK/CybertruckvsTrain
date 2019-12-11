//  Name: PauseMenu.cs
//  Author: Connor Larsen
//  Purpose:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pausePanel, gamePanel;

    public AudioMixer audioMixer;

    private void Awake()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        audioMixer.SetFloat("GameVolume", 0);
    }

    public void MainMenu()
    {
        gamePaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    private void PauseGame()
    {
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        audioMixer.SetFloat("GameVolume", -80); 
    }
}