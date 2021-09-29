using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControllerMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> panels = new List<GameObject>();

    public static UIControllerMenu instance;
    private PlayerController playerController;
    public Text scoreText, highScoreText;
    public int highScore;

    private void Awake()
    {
        instance = this;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.text = "High Score: " + highScore;
        }
    }

    // Makes sure the starting panel appears at the start of the game
    private void Start()
    {
        setActivePanel(0);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        scoreText.text = "Current Score: 0";
    }
    private void Update()
    {
        scoreText.text = "Current Score: " + playerController.score;
    }
    // Quits the game
    public void OnQuitButtonClick()
    {
        print("Quitting the game");
        Application.Quit();
    }

    // These functions display the correct panel
    public void OnRulesButtonClick()
    {
        setActivePanel(1);
        FindObjectOfType<AudioManager>().Play("Click");
    }
    public void OnCreditsButtonClick()
    {
        setActivePanel(2);
        FindObjectOfType<AudioManager>().Play("Click");
    }
    public void OnBackButtonClick()
    {
        setActivePanel(0);
        FindObjectOfType<AudioManager>().Play("Click");
    }
    public void OnLevelsButtonClick()
    {
        setActivePanel(3);
        FindObjectOfType<AudioManager>().Play("Click");
    }

    // Loads the next scene
    public void OnLevelButtonClick()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Returns to the main menu
    public void OnRetryButtonClick()
    {
        SceneManager.LoadScene(0);
        ResetScore();
    }

    // Sets the active panel to the specified panel
    public void setActivePanel(int panelNumber)
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        panels[panelNumber].SetActive(true);
    }

    public void UpdateHighScore()
    {
        if(playerController.score > highScore)
        {
            highScore = playerController.score;
            highScoreText.text = "High Score: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void ResetScore()
    {
        playerController.score = 0;
    }
}
