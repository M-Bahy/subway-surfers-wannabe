using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{


    public TMP_Text scoreText; // Score : 0
     public Button restartButton;

    public Button mainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        
        scoreText.text = "Final Score : " + Mathf.FloorToInt(PlayerScript.score).ToString();
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void GoToMainMenu()
    {
        Time.timeScale = 1f;
       SceneManager.LoadScene("Title Screen"); 
    }
      public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); 
    }
}
