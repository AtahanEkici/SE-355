using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public int Score;

    public Text scoreText;
    public Text bestScoreText;
    public Button restartButton;
    public GameObject gameOver;

    void Awake()
    {
        if (PlayerPrefs.HasKey("BestScore") == false)
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }
    }
    void Update()
    {
        scoreText.text = "Score : " + PlayerPrefs.GetInt("Score");
        bestScoreText.text = "Best Score : " + PlayerPrefs.GetInt("BestScore");
        CalculateBestScore();
    }
    void CalculateBestScore()
    {
        if (PlayerPrefs.GetInt("BestScore") < Score)
        {
            PlayerPrefs.SetInt("BestScore", Score);
        }
    }
    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver() 
    {
        gameOver.SetActive(true);
    }
    public void addScore()
    {
        Score++;
        scoreText.text = "Score : " + Score;
        PlayerPrefs.SetInt("Score", Score);
    }
}
