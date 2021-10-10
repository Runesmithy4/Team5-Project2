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

    // Makes sure the starting panel appears at the start of the game
    private void Start()
    {
        setActivePanel(0);
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
    public void OnFirstLevelButtonClick()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Loads the next scene
    public void OnSecondLevelButtonClick()
    {
        if (SceneManager.GetActiveScene().buildIndex + 2 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

    // Loads the next scene
    public void OnThirdLevelButtonClick()
    {
        if (SceneManager.GetActiveScene().buildIndex + 3 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
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
}
