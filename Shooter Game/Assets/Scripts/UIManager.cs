using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    // variables
    private int score;
    private int highScore;
    private int maxHealth;
    //end of variables



    // game objects
    public TMP_Text currentScore;
    public TMP_Text gameOverCurrentScore;
    public TMP_Text highScoreText;
    public Slider healthBarSlider;
    //end of game objects

    // classes
    public PlayerController playerControllerScript;
    // end of classes

    // Start is called before the first frame update
    void Start()
    {

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = playerControllerScript.score;

        currentScore.text = " Score: " + score;
        gameOverCurrentScore.text = " Score: " + score;

        maxHealth = playerControllerScript.maxHealth;
        SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {

        score = playerControllerScript.score;
        highScore = playerControllerScript.highScore;
        currentScore.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
        gameOverCurrentScore.text = " Score: " + score;

    }


    public void SetMaxHealth(int health)
    {

        healthBarSlider.maxValue = health;
        healthBarSlider.value = health;

    }

    public void SetHealth(int health)
    {

        healthBarSlider.value = health;

    }

    // Game Over panel
    public void StartGame()
    {

        SceneManager.LoadScene("GameScene");

    }

    public void MainMenu()
    {

        SceneManager.LoadScene("MainMenuScene");

    }

}
