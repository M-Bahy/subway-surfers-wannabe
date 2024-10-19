using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleScript : MonoBehaviour
{

    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public Button muteButton;

    bool isMuted = false;

    // Start is called before the first frame update
    void Start()
    {
        //set the buttons to active
        // playButton.gameObject.SetActive(true);
        // optionsButton.gameObject.SetActive(true);
        // quitButton.gameObject.SetActive(true);
        // connect the buttons to the functions
        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(Options);
        quitButton.onClick.AddListener(QuitGame);
        muteButton.onClick.AddListener(Mute);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void Options()
    {
        // Load the options scene
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Options");
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    public void Mute()
    {
        // Mute the game
        if (isMuted)
        {
            isMuted = false;
            AudioListener.volume = 1f;
        }
        else
        {
            isMuted = true;
            AudioListener.volume = 0f;
        }
    }
}
