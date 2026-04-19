using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogicScript : MonoBehaviour
{
    [SerializeField]
    GameObject NGConfirmationWindow, creditsWindow;
    public void StartGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void OpenNewGameConfirmation()
    {
        NGConfirmationWindow.SetActive(true);
    }
    public void CloseNewGameConfirmation()
    {
        NGConfirmationWindow.SetActive(false);
    }

    public void OpenCreditsWindow()
    {
        creditsWindow.SetActive(true);
    }
    public void CloseCreditsWindow()
    {
        creditsWindow.SetActive(false);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            CloseNewGameConfirmation();
            CloseCreditsWindow();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
