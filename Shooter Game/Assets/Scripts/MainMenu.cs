using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // game objects
    public GameObject creditsPanel;
    // end of game objects


    public void StartGame()
    {

        SceneManager.LoadScene("GameScene");

    }

    public void OpenCredits()
    {

        creditsPanel.SetActive(true);

    }

    public void CloseCredits()
    {

        creditsPanel.SetActive(false);

    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();

    }


}
