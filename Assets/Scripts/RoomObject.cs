using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
public class RoomObject : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] bool doStuff;
    [SerializeField] bool isStartingRoom;

    [SerializeField] int deadSpawnPointIndex;
    
    StepForward stepForward;
    int generationSpawned;

    bool roomDead = false;
    
    // private Transform spawnPointsObject;

    void Start()
    {
        stepForward = FindObjectOfType<StepForward>();

        generationSpawned = stepForward.GetCurrentGeneration()+1;
    }

    void Update()
    {
        if((doStuff) && (!roomDead) && (stepForward.GetCurrentGeneration() == generationSpawned)){

            int currentIndex = 0;
            foreach (SpawnPoint spawnPoint in spawnPoints){
                Debug.Log("loop "+currentIndex);
                if(deadSpawnPointIndex == currentIndex){
                    //Debug.Log(deadSpawnPointIndex);
                    //Debug.Log(currentIndex);
                    //Debug.Log(spawnPoints);
                }
                else{
                    spawnPoint.ActivateSpawnPoint();
                }

                currentIndex++;
            }
            roomDead = true;
        }
    }

    public void Setup(bool doStufff, int deedSpawnPointtIndex){
        doStuff = doStufff;
        deadSpawnPointIndex = deedSpawnPointtIndex;
        Debug.Log("deadspawn "+deadSpawnPointIndex);
    }
}