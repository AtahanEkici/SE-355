using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Atahan Ekici //
// Onat Kocabaþoðlu //

public class GameManager : MonoBehaviour
{
    private static GameManager _instance; // instance of this GameManager object for singleton pattern //
    private AsyncOperation AsyncLoadLevel; // Asyncronous object for scene loading //

    public string CurrentSceneName;  // Current Scene's name //
    public GameObject Player; // Player Object //
    public bool DontDestroyStatus = true; // Don't destroy on default //

    public Toggle toggleButton; // V-Synch toggle button //

    public Canvas Score_Canvas;
    public Canvas Menu_Canvas;

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
    }
    private void Start()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;

        toggleButton.onValueChanged.AddListener(delegate  // attach listener to the toggle button //
        {
            ToggleValueChanged(toggleButton);
        });
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
        }

        else if (SceneManager.GetActiveScene().name != CurrentSceneName)
        {
            Debug.Log("Scene Changed: " + scene.name);
            CurrentSceneName = SceneManager.GetActiveScene().name;
        }

        else
        {
            Debug.Log("Scene Restart: " + scene.name);
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("Scene terminated: " + CurrentSceneName + "");
    }

    private void Update()
    {
        OpenMenu(KeyCode.P);
    }

    public void ToggleValueChanged(Toggle change) 
    {
        if(change.isOn == true)
        {
            QualitySettings.vSyncCount = 1;
            Debug.Log("V-Sync is now enabled  " + QualitySettings.vSyncCount + "");
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Debug.Log("V-Sync is now disabled  " + QualitySettings.vSyncCount + "");
        }      
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
    private void OpenMenu(KeyCode k)
    {
        if(Input.GetKeyDown(k))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0; // Pause the game loop //
                Score_Canvas.gameObject.SetActive(false); // hide overlay //
                Menu_Canvas.gameObject.SetActive(true); // display menu //
            }
            else
            {
                Time.timeScale = 1; // Resume the game loop //
                Score_Canvas.gameObject.SetActive(true); // display overlay //
                Menu_Canvas.gameObject.SetActive(false); // hide menu //
            }
           
        }
    }

    public void Force_Frame_Rate(int given_frame_rate) // This function is for debugging purposes only  also it effects inspector too so use with caution//
    {
        Application.targetFrameRate = given_frame_rate;
    }
}