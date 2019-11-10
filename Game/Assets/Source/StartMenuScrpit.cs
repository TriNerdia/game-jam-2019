using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuScrpit : MonoBehaviour
{
    public Button startBtn;

    void Start()
    {
        Button rbtn = startBtn.GetComponent<Button>();
        rbtn.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Maze 1", LoadSceneMode.Single);
    }
}
