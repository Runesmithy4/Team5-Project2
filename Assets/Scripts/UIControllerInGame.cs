using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControllerInGame : MonoBehaviour
{
    [SerializeField] List<GameObject> panels = new List<GameObject>();

    public static UIControllerMenu instance;
    private PlayerController playerController;
    public Text scoreText, highScoreText;
    public int highScore;

    // Makes sure the starting panel appears at the start of the game
    private void Start()
    {
        print(PlayerPrefs.GetInt("Highscore1").ToString());
        print(PlayerPrefs.GetInt("Highscore2").ToString());
        print(PlayerPrefs.GetInt("Highscore3").ToString());

        setActivePanel(0);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        scoreText.text = "Current Score: 0";
        highScoreText.text = "Highscore: " + GetHighscore();
    }

    private void Update()
    {
        scoreText.text = "Current Score: " + playerController.score;
    }

    // Returns to the main menu
    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    // Quits the game
    public void OnQuitButtonClick()
    {
        Application.Quit();
    }    

    // Reloads the current level
    public void OnRetryButtonClick()
    {
        UpdateHighScore(highScoreText);
        ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void ResetScore()
    {
        playerController.score = 0;
    }

    private string GetHighscore()
    {
        string highscoreString = "0";

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            highscoreString = PlayerPrefs.GetInt("Highscore1", 0).ToString();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            highscoreString = PlayerPrefs.GetInt("Highscore2", 0).ToString();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            highscoreString = PlayerPrefs.GetInt("Highscore3", 0).ToString();
        }

        return highscoreString;
    }

    public void UpdateHighScore(Text highScoreText)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (playerController.score > PlayerPrefs.GetInt("Highscore1"))
            {
                PlayerPrefs.SetInt("Highscore1", playerController.score);
            }
            highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore1").ToString();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (playerController.score > PlayerPrefs.GetInt("Highscore2"))
            {
                PlayerPrefs.SetInt("Highscore2", playerController.score);
            }
            highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore2").ToString();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (playerController.score > PlayerPrefs.GetInt("Highscore3"))
            {
                PlayerPrefs.SetInt("Highscore3", playerController.score);
            }
            highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore3").ToString();
        }
    }
}
