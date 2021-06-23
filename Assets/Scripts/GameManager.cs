using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Atahan Ekici //
// Onat Kocabaþoðlu //

public class GameManager : MonoBehaviour
{
    public bool DontDestroyStatus = true; // Don't destroy on default //
    public string CurrentSceneName;  // Current Scene's name //
    public GameObject Player; // Player Object //
    public float velocity_limit;

    private static GameManager _instance; // instance of this GameManager object for singleton pattern //
    private AsyncOperation AsyncLoadLevel; // Asyncronous object for scene loading //
    private Rigidbody2D rb;

    public UIManager UI;
    public GameObject Score_Canvas; // Score Overlay //
    public GameObject Menu_Canvas; // Pause Menu //
    public GameObject Start_Menu; // Start Menu //

    public static GameManager Instance
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

            if (DontDestroyStatus == true)
            {
                DontDestroyOnLoad(this);
            }
        }
        Init();
    }

    private void OnEnable() // This function is called upon gameobject activation //
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // This function is called after the scene changed //
    {
        if (CurrentSceneName == null || CurrentSceneName == "")
        {
            Debug.Log("Game Started on Scene: " + scene.name);
            CurrentSceneName = SceneManager.GetActiveScene().name;
            Start_Menu.SetActive(true);
            Time.timeScale = 0;
        }

        else if (SceneManager.GetActiveScene().name != CurrentSceneName)
        {
            Debug.Log("Scene Changed: " + scene.name);
            CurrentSceneName = SceneManager.GetActiveScene().name;
        }

        else
        {
            Debug.Log("Scene Restart: " + scene.name);
            Init();
        }
    }
    private void Init()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = Player.GetComponent<Rigidbody2D>();
        Score_Canvas = GameObject.Find("Overlay");
        UI = Score_Canvas.GetComponent<UIManager>();
        Menu_Canvas = UI.Pause_Menu;
        Start_Menu = UI.Start_Menu;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("Scene terminated: " + CurrentSceneName + "");
    }

    private void Update()
    {
        OpenMenu(KeyCode.P);
        LimitVelocity();
    }

    public void OnLevelFinish()
    {
        Debug.Log("Scene finished: " + SceneManager.GetActiveScene().name + "");
    }
    public void ReturnSceneName(KeyCode key) // Debugging Function Delete on Release Build //
    {
        if (Input.GetKeyDown(key))
        {
            Debug.Log("" + SceneManager.GetActiveScene().name + "");
        }
    }
    private void LimitVelocity()
    {
        if (rb.velocity.magnitude > velocity_limit)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, velocity_limit);
        }
    }
    public void RestartCurrentScene() // restarts the active scene //
    {
        CurrentSceneName = SceneManager.GetActiveScene().name; // Get Current Scene's Name //
        SceneManager.LoadScene(CurrentSceneName); // Load the scene //
        //Debug.Log("Restart Conducted");
    }
    public void QuitGame()
    {
        Application.Quit(); // Does not work in inspector works only on game builds //
    }
    public IEnumerator Load_Level(string SceneName)
    {
        OnLevelFinish();

        AsyncLoadLevel = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);

        while (AsyncLoadLevel.isDone) // wait until the level is loaded //
        {
            yield return null;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Restart_Game()
    {
        StartCoroutine(Load_Level(CurrentSceneName));
    }
    public void OpenMenu(KeyCode k)
    {
        if (Input.GetKeyDown(k))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0; // Pause the game loop //
                Score_Canvas.SetActive(false); // hide overlay //
                Menu_Canvas.SetActive(true); // display menu //
            }
            else
            {
                Time.timeScale = 1; // Resume the game loop //
                Score_Canvas.SetActive(true); // display overlay //
                Menu_Canvas.SetActive(false); // hide menu //
            }
        }
    }
    public void Force_Frame_Rate(int given_frame_rate) // This function is for debugging purposes only  also it effects inspector too so use with caution//
    {
        Application.targetFrameRate = given_frame_rate;
    }
}