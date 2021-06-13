using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public float speed = 10f;

    private Material player_material;
    private Material trail_material;

    private float timeLeft = 2f;
    private Color targetColor;

    private void Awake()
    {
        player_material = GetComponent<Renderer>().material;
        trail_material = GetComponent<TrailRenderer>().material;
        targetColor = Random.ColorHSV();
    }
    private void Update()
    {
        ColorController();
    }

    private Color GetColor()
    {
        return player_material.GetColor("_Color");
    }

    private void SetColor(Color wanted_color)
    {
        player_material.SetColor("_Color", wanted_color);
    }
    private Color Color_Inverter(Color player_color)
    {
        return new Color((1 - player_color.r), (1 - player_color.g), (1 - player_color.b), 1);
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
            trail_material.SetColor("_Color", temp);
            timeLeft -= Time.deltaTime;
        }
    }
}
