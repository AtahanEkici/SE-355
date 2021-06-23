using UnityEngine;
using Vector2 = UnityEngine.Vector2;
// Atahan Ekici //
// Onat Kocabaşoğlu //

public class Player : MonoBehaviour
{
    public GameObject particlePrefab;
    public CameraShake camshake;

    private Rigidbody2D rb;
    private int xSpeed = 25;
    private int ySpeed = -2;
    private UIManager uiManager;
    private Material player_renderer_material;
    private static Player _instance;
    public static Player Instance
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

        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
        rb = GetComponent<Rigidbody2D>();
        player_renderer_material = GetComponent<Renderer>().material;
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
            rb.AddForce(new Vector2(xSpeed, ySpeed));

    }
    void GetInput() 
    {
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && Time.timeScale > 0) 
        {
                rb.AddForce(new Vector2(-xSpeed * 3, 0));
            
                //rb.AddForce(new Vector2(xSpeed * 3, 0));
            
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
        this.gameObject.SetActive(false);
        uiManager.GameOver();
        camshake.InduceStress(2f, 1f, 2f); // Camera Shake //
        ChangeColor(GetPlayerColor(), particlePrefab.GetComponent<Renderer>()); // Change color of the particle //
        Destroy(Instantiate(particlePrefab, transform.position, Quaternion.identity), 10f); // Particle Effect //
    }
}
