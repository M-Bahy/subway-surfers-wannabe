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
    public GameObject htpPanel;
    public GameObject creditsPanel;
    public Button soundButton;
    public Button howToPlayButton;
    public Button creditsButton;
    public Button backButton;
     public Button htpBackButton;
     public Button creditsBackButton;
    bool isMuted = false;
     public TMP_Text soundText;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        htpPanel.SetActive(false);
        creditsPanel.SetActive(false);
        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(Options);
        quitButton.onClick.AddListener(QuitGame);
        soundButton.onClick.AddListener(Mute);
        howToPlayButton.onClick.AddListener(explainTheGame);
        creditsButton.onClick.AddListener(creditsWhenCreditsAreDue);
        backButton.onClick.AddListener(Back);
        htpBackButton.onClick.AddListener(HtpBack);
        creditsBackButton.onClick.AddListener(CreditsBack);
       if (AudioListener.volume == 0f)
        {
            isMuted = true;
            soundText.text = "Sound : OFF";
        }
        else
        {
            isMuted = false;
            soundText.text = "Sound : ON";
        }
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
        htpPanel.SetActive(false);
        creditsPanel.SetActive(false);
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
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        htpPanel.SetActive(true);
        htpBackButton.OnDeselect(null);

    }
    public void creditsWhenCreditsAreDue(){
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        htpPanel.SetActive(false);
        creditsPanel.SetActive(true);
        creditsBackButton.OnDeselect(null);
        
    }
    public void Back()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        htpPanel.SetActive(false);
        backButton.OnDeselect(null);
    }
     public void HtpBack()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(true);
        htpPanel.SetActive(false);
        htpBackButton.OnDeselect(null);
    }

    public void CreditsBack()
    {
       mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(true);
        htpPanel.SetActive(false);
        creditsBackButton.OnDeselect(null);
    }
    
        
    
}
