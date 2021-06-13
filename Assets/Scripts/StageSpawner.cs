using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{

    StageManager stageManager;

    // Start is called before the first frame update
    void Start()
    {
        stageManager = (StageManager)FindObjectOfType(typeof(StageManager));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            stageManager.InstantiateStages();  
        }
    }
}
