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

    // Quits the game
    public void OnQuitButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    // Returns to the main menu
    public void OnRetryButtonClick()
    {
        UpdateHighScore();
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
            highscoreString = PlayerPrefs.GetInt("Highscore1").ToString();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            highscoreString = PlayerPrefs.GetInt("Highscore2").ToString();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            highscoreString = PlayerPrefs.GetInt("Highscore3").ToString();
        }

        return highscoreString;
    }

    public void UpdateHighScore()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (playerController.score > PlayerPrefs.GetInt("Highscore1"))
            {
                PlayerPrefs.SetInt("Highscore1", playerController.score);
                highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore1").ToString();
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (playerController.score > PlayerPrefs.GetInt("Highscore2"))
            {
                PlayerPrefs.SetInt("Highscore2", playerController.score);
                highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore2").ToString();
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (playerController.score > PlayerPrefs.GetInt("Highscore3"))
            {
                PlayerPrefs.SetInt("Highscore3", playerController.score);
                highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore3").ToString();
            }
        }
    }

    public void ResetHighscore()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayerPrefs.DeleteKey("Highscore1");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            PlayerPrefs.DeleteKey("Highscore2");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            PlayerPrefs.DeleteKey("Highscore3");
        }
        
        highScoreText.text = "0";
    }
}
