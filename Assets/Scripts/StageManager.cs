using UnityEngine;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class StageManager : MonoBehaviour
{
    public GameObject[] stagePrefabs;
    public GameObject playerObj;
    public int Score = 0;
    public int bestScore;

    private int playerDistanceIndex = -1;
    private int stageIndex = 0;
    private int distanceToNext = 10;
    private int randStage;
    private GameObject newStage;
    public List<GameObject> instances = new List<GameObject>();
    private static StageManager _instance;

    private static StageManager Instance
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
    }
    private void Update()
    {
        int playerDistance = (int)(playerObj.transform.position.y / (distanceToNext / 2));

        if(playerDistanceIndex != playerDistance) 
        {
            InstantiateStages();
            playerDistanceIndex = playerDistance;
        }
    }
    public void InstantiateStages() 
    {
            randStage = Random.Range(0, stagePrefabs.Length);
            newStage = Instantiate(stagePrefabs[randStage], new Vector3(0, stageIndex * -distanceToNext), Quaternion.identity);   
            newStage.transform.SetParent(transform);
            instances.Add(newStage);
            stageIndex++;
    }
}
