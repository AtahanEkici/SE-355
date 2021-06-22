using UnityEngine;
public class Changer : MonoBehaviour
{
    public Player playerScript;
    public UIManager uiManager;
    private void Awake()
    {
        playerScript = (Player)FindObjectOfType(typeof(Player));
        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            uiManager.addScore();
        }
    }
}
