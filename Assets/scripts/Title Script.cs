using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScript : MonoBehaviour
{

    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public Button soundButton;
    public Button howToPlayButton;
    public Button creditsButton;
    public Button backButton;
    bool isMuted = false;
     public TMP_Text soundText;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(Options);
        quitButton.onClick.AddListener(QuitGame);
        soundButton.onClick.AddListener(Mute);
        howToPlayButton.onClick.AddListener(explainTheGame);
        creditsButton.onClick.AddListener(creditsWhenCreditsAreDue);
        backButton.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        // Load the game scene
        SceneManager.LoadScene("SampleScene");
        playButton.OnDeselect(null);
    }

    public void Options()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        optionsButton.OnDeselect(null);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
        quitButton.OnDeselect(null);
    }

    public void Mute()
    {
        // Mute the game
        if (isMuted)
        {
            isMuted = false;
            AudioListener.volume = 1f;
            soundText.text = "Sound : ON";
            
            
        }
        else
        {
            isMuted = true;
            AudioListener.volume = 0f;
            soundText.text = "Sound : OFF";
        }
        // mark the button as not pressed
        soundButton.OnDeselect(null);
    }

    public void explainTheGame(){

    }
    public void creditsWhenCreditsAreDue(){
        
    }
    public void Back()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        backButton.OnDeselect(null);
    }
}
