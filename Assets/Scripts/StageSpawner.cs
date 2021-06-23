using UnityEngine;
// Atahan Ekici //
// Onat Kocabaşoğlu //
public class StageSpawner : MonoBehaviour
{
    private StageManager stageManager;
    private void Awake()
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
