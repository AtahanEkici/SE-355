using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class StageManager : MonoBehaviour
{
    public GameObject[] stagePrefabs;
    public GameObject playerObj;
    public int Score = 0;
    public int bestScore;

    int playerDistanceIndex = -1;
    int stageIndex = 0;
    int distanceToNext = 10;
    int randStage;

    private GameObject newStage;

    void Update()
    {
        int playerDistance = (int)(playerObj.transform.position.y / (distanceToNext / 2));

        if(playerDistanceIndex != playerDistance) 
        {
            InstantiateStages();
            Debug.Log("Geçti");
            playerDistanceIndex = playerDistance;
        }
    }
    public void InstantiateStages() 
    {
            randStage = Random.Range(0, stagePrefabs.Length);
            newStage = Instantiate(stagePrefabs[randStage], new Vector3(0, stageIndex * -distanceToNext), Quaternion.identity);
            newStage.transform.SetParent(transform);
            stageIndex++;
        //Destroy(stagePrefabs[0]);
    }

}
