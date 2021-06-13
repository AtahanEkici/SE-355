using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    public GameObject player;
    Rigidbody2D rb;
    int randMove;
    int xSpeed = 25;
    int ySpeed = -2;

    public UIManager uiManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
    }
    private void Update()
    {
        MovePlayer();
        GetInput();
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
        if (Input.GetMouseButton(0)) 
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

    public int randomizeNumber() 
    {
        randMove = Random.Range(1, 10);
        Debug.Log("Move Code: " + randMove);
        return randMove;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Wall") 
        {
            endGame();
        }
    }
    public void endGame() 
    {
        player.SetActive(false);
        uiManager.GameOver();
    }

}
