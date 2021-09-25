using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControllerMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> panels = new List<GameObject>();

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
    }
    public void OnCreditsButtonClick()
    {
        setActivePanel(2);
    }
    public void OnBackButtonClick()
    {
        setActivePanel(0);
    }
    public void OnLevelsButtonClick()
    {
        setActivePanel(3);
    }

    // Loads the next scene
    public void OnLevelButtonClick()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
