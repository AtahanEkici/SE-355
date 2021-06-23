using UnityEngine.UI;
using UnityEngine;
// Atahan Ekici //
// Onat Kocabaþoðlu //

public class Remember : MonoBehaviour
{
    public Toggle V_Sync_Toggle;
    public Toggle Color_Lerp_Toggle;

    public GameObject Score_Canvas;
    public GameObject Menu_Canvas;

    public InputField input;
    public string input_string;
    public Text confirmation;

    public static readonly string Color_Lerp = "Color_Lerp_Status";
    public static readonly string V_Sync = "V_Synch_Status";
    public static readonly string Velocity = "Max_Velocity";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(V_Sync) == false)
        {
            PlayerPrefs.SetInt(V_Sync,0);
        }
        else
        {
            if (PlayerPrefs.GetInt(V_Sync) == 0)
            {
                QualitySettings.vSyncCount = 0;
            }
            else
            {
                QualitySettings.vSyncCount = 1;
            }
        }

        if (PlayerPrefs.HasKey(Color_Lerp) == false)
        {
            PlayerPrefs.SetInt(Color_Lerp,1);
        }
        else
        {
            if (PlayerPrefs.GetInt(Color_Lerp) == 1)
            {
                Color_Lerp_Toggle.isOn = true;
            }
            else
            {
                Color_Lerp_Toggle.isOn = false;
            }
        }

        if(PlayerPrefs.HasKey(Velocity) == false)
        {
            PlayerPrefs.SetFloat(Velocity,5f);
        }
        else
        {
            GameObject gm = GameObject.Find("GameManager");
            gm.GetComponent<GameManager>().velocity_limit = PlayerPrefs.GetFloat(Velocity);
        }
    }
    private void Start()
    {
        Debug.Log("V-Sync" + PlayerPrefs.GetInt(V_Sync));
        Debug.Log("Color_lerp" + PlayerPrefs.GetInt(Color_Lerp));

        V_Sync_Toggle.onValueChanged.AddListener(delegate  // attach listener to the toggle button //
        {
            V_Sync_Template();
        });

        Color_Lerp_Toggle.onValueChanged.AddListener(delegate  // attach listener to the toggle button //
        {
            Color_Lerp_Template();
        });
    }

    public void Clear_Text()
    {
        confirmation.text = "";
    }

    public void OpenMenu() // For Overlay Button since it does not accept parametered functions//
    {
        Debug.Log("Open Menu is called");

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

    public void Get_Velocity_Limit()
    {
        input_string = input.text;
        float velocity = float.Parse(input_string);

        Debug.Log(velocity);

        if (velocity >= 25f)
        {
            velocity = 25f;
        }
        else if (velocity <= 0f)
        {
            velocity = 5f;
        }

        PlayerPrefs.SetFloat(Velocity, velocity);
        GameObject gm = GameObject.Find("GameManager");
        gm.GetComponent<GameManager>().velocity_limit = velocity;
        string temp = "Max Velocity set to " + velocity + "";
        Debug.Log(temp);
        confirmation.text = temp;
        Invoke(nameof(Clear_Text), 3f);
        input.text = "";
    }   
    private void V_Sync_Template()
    {

        if (V_Sync_Toggle.isOn == true)
        {
            PlayerPrefs.SetInt(V_Sync, 1);
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            PlayerPrefs.SetInt(V_Sync,0);
            QualitySettings.vSyncCount = 0;
        }
    }
    private void Color_Lerp_Template()
    {
        if (Color_Lerp_Toggle.isOn == true)
        {
            PlayerPrefs.SetInt(Color_Lerp,1);
        }
        else
        {
            PlayerPrefs.SetInt(Color_Lerp,0);
            Color_Lerp_Toggle.isOn = false;
        }
    }
}
