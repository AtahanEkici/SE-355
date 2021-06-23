using UnityEngine.UI;
using UnityEngine;
// Atahan Ekici //
// Onat Kocabaþoðlu //

public class Remember : MonoBehaviour
{
    public Toggle V_Sync_Toggle;
    public Toggle Color_Lerp_Toggle;

    public static readonly string Color_Lerp = "Color_Lerp_Status";
    public static readonly string V_Sync = "V_Synch_Status";

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
