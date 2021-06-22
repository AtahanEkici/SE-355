using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public int Score = 0;
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
    private void Start()
    {
        scoreText.text = "Score : " + Score;
    }
    private void Update()
    {
        scoreText.text = "Score : " + Score.ToString();
        bestScoreText.text = "Best Score : " + PlayerPrefs.GetInt("BestScore");
        CalculateBestScore();
    }
    private void CalculateBestScore()
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
