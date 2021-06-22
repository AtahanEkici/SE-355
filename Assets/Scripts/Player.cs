using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    public GameObject particlePrefab;
    public GameObject player;
    public CameraShake camshake;

    private Rigidbody2D rb;
    private int randMove;
    private int xSpeed = 25;
    private int ySpeed = -2;
    private UIManager uiManager;
    private Material player_renderer_material;

    private void Awake()
    {
        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
        rb = GetComponent<Rigidbody2D>();
        player_renderer_material = player.GetComponent<Renderer>().material;
    }
    private void FixedUpdate()
    {
        MovePlayer(); 
    }
    private void Update()
    {
        GetInput();
    }
    private Color GetPlayerColor()
    {
            return player_renderer_material.GetColor("_Color");
    }
    private void ChangeColor(Color color, Renderer renderer)
    {
        if (renderer.gameObject != null)
        {
            renderer.sharedMaterial.SetColor("_Color", color);
        }
    }
    public void MovePlayer() 
    {
        if(randMove <= 5) 
        {
            rb.AddForce(new Vector2(xSpeed, ySpeed));
        }
        else
        {
            rb.AddForce(new Vector2(-xSpeed, ySpeed));
        }
    }
    void GetInput() 
    {
        if (Input.GetMouseButton(0) && Time.timeScale > 0) 
        {
            if(randMove <= 5) 
            {
                rb.AddForce(new Vector2(-xSpeed * 3, 0));
            }
            else 
            {
                rb.AddForce(new Vector2(xSpeed * 3, 0));
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Wall")) 
        {
            endGame();
        }
    }
    public void endGame() 
    {
        player.SetActive(false);
        uiManager.GameOver();
        camshake.InduceStress(2f, 1f, 2f); // Camera Shake //
        ChangeColor(GetPlayerColor(), particlePrefab.GetComponent<Renderer>()); // Change color of the particle //
        Destroy(Instantiate(particlePrefab, transform.position, Quaternion.identity), 10f); // Particle Effect //
    }
}
