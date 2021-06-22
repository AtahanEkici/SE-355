using UnityEngine;
public class StageSpawner : MonoBehaviour
{
    private StageManager stageManager;
    void Start()
    {
        stageManager = (StageManager)FindObjectOfType(typeof(StageManager));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stageManager.InstantiateStages();  
        }
    }
}
