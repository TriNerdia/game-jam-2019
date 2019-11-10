using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuScrpit : MonoBehaviour
{
    public Button startBtn;
    public Button quitBtn;

    void Start()
    {
        Button rbtn = startBtn.GetComponent<Button>();
        Button qbtn = quitBtn.GetComponent<Button>();
        rbtn.onClick.AddListener(StartGame);
        qbtn.onClick.AddListener(QuitGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Maze 1", LoadSceneMode.Single);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
