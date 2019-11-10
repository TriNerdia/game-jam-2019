using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{

    public static bool PortalsDestroyed { get => GameObject.FindGameObjectsWithTag("Portal").Length == 0; }

    public static int PortalsLeft { get => GameObject.FindGameObjectsWithTag("Portal").Length; }

    [SerializeField] TMP_Text displayText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                pauseGame();
            }
            else
            {
                coutinueGame();
            }
        }
    }

    void pauseGame()
    {
        Time.timeScale = 0;
        displayText.gameObject.SetActive(true);
        displayText.text = "Paused";
        displayText.fontSize = 32;
        displayText.alignment = TextAlignmentOptions.Center;
    }

    void coutinueGame()
    {
        Time.timeScale = 1;
        displayText.fontSize = 18;
        displayText.alignment = TextAlignmentOptions.Left;
        displayText.gameObject.SetActive(false);
    }
}
