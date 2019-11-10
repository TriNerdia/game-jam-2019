using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverMenuScript : MonoBehaviour
{
    public Button restartBtn;

    void Start()
    {
        Button rbtn = restartBtn.GetComponent<Button>();
        rbtn.onClick.AddListener(Scenechange);
    }

    void Scenechange()
    {
        SceneManager.LoadScene("Opening Menu", LoadSceneMode.Single);
    }
}
