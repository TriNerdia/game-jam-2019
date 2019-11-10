using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverMenuScript : MonoBehaviour
{
    public Button restartBtn;
    public Button menuBtn;
    public Button quitBtn;

    void Start()
    {
        Button rbtn = restartBtn.GetComponent<Button>();
        Button mbtn = menuBtn.GetComponent<Button>();
        Button qbtn = quitBtn.GetComponent<Button>();
        rbtn.onClick.AddListener(ScenechangeGame);
        mbtn.onClick.AddListener(ScenechangeMenu);
        qbtn.onClick.AddListener(QuitGame);
    }

    void ScenechangeMenu()
    {
        SceneManager.LoadScene("Opening Menu", LoadSceneMode.Single);
    }

    void ScenechangeGame()
    {
        SceneManager.LoadScene("Maze 1", LoadSceneMode.Single);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
