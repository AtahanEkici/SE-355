using UnityEngine;
using UnityEngine.UI;
// Atahan Ekici //
// Onat Kocabaşoğlu //

public class ColorManager : MonoBehaviour
{
    public StageManager stage_manager;
    public bool Color_Controll;
    public Toggle toggleButton;

    private Material player_material;
    private Material trail_material;
    private float timeLeft = 2f;
    private Color targetColor;

    private void Awake()
    {
        player_material = GetComponent<Renderer>().material;
        trail_material = GetComponent<TrailRenderer>().material;
        targetColor = Random.ColorHSV();
        Color_Setting_Checker();
    }
    private void Start()
    {
        toggleButton.onValueChanged.AddListener(delegate  // attach listener to the toggle button //
        {
            ToggleValueChanged(toggleButton);
        });

        player_material.color = Random.ColorHSV();
        ColorProcess(Random.ColorHSV());
    }
    private void Update()
    {
        if(Color_Controll == true)
        {
            ColorController();
        }
    }
    public void ToggleValueChanged(Toggle change)
    {
        if (change.isOn == true)
        {
            Color_Controll = true;
            Debug.Log("Color is not changing at this point");
        }
        else
        {
            Color_Controll = false;
            Debug.Log("Color is changing at this point");
        }
    }
    private void Color_Setting_Checker()
    {
        if (PlayerPrefs.GetInt(Remember.Color_Lerp) == 1)
        {
            Color_Controll = true;
        }
        else
        {
            Color_Controll = false;
        }

    }
    private Color GetColor()
    {
        return player_material.GetColor("_Color");
    }
    private void SetColor(SpriteRenderer render,Color wanted_color)
    {
        render.material.SetColor("_Color", wanted_color);
    }
    private Color Color_Inverter(Color player_color)
    {
        return new Color((1 - player_color.r), (1 - player_color.g), (1 - player_color.b), 1);
    }
    private void ColorProcess(Color color)
    {
        for (int i = 0; i < stage_manager.instances.Count; i++)
        {
            SpriteRenderer[] components = stage_manager.instances[i].GetComponentsInChildren<SpriteRenderer>();

            for (int j = 0; j < components.Length; j++)
            {
                SetColor(components[j], color);
            }
        }
    }
    private void ColorController()
    {
        if (timeLeft <= Time.deltaTime)
        {
            player_material.color = targetColor;
            trail_material.color = Color_Inverter(targetColor);
            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = 1.0f;
        }
        else
        {
            player_material.color = Color.Lerp(player_material.color, targetColor, Time.deltaTime / timeLeft);
            Color temp = Color.Lerp(trail_material.color, Color_Inverter(targetColor), Time.deltaTime / timeLeft);
            ColorProcess(temp);
            trail_material.SetColor("_Color", temp);
            timeLeft -= Time.deltaTime;
        }
    }
}
