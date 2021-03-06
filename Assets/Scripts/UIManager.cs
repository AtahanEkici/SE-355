using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Atahan Ekici //
// Onat Kocabaşoğlu //

public class UIManager : MonoBehaviour
{
    public int Score = 0;
    public Text scoreText;
    public Text bestScoreText;
    public Button restartButton;
    public GameObject gameOver;
    public GameObject Start_Menu;
    public GameObject Pause_Menu;
    public Button Pause_Menu_Button;
    public Canvas this_canvas;

    private static UIManager _instance;

    public static UIManager Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

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
    public void StartGame()
    {
        Start_Menu.SetActive(false);
        Time.timeScale = 1;
        this_canvas.enabled = true;
        Pause_Menu_Button.enabled = true;
    }
}
