using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> rooms = new List<GameObject>();
    [SerializeField] GameObject startingRoom;

    GameObject startingRoomInstance;

    public int spawnPointIndexReturn;

    int hardStop = 0;
    int hardStopMax = 10;

    int currentSpawnPointID = 0;

    void Start()
    {
        //find rooms
        foreach (GameObject go in Resources.LoadAll<GameObject>("Rooms")){
            if (go.tag == "Room"){
                rooms.Add(go);
            }
        }

        //spawn starting room
        startingRoomInstance = Instantiate(startingRoom, transform.position, Quaternion.identity);

        //myObj = GameObject.FindGameObjectsWithTag("Room");
    }

    void Update()
    {
        
    }

    public SpawnPoint SpawnRoom(SpawnPoint roomSpawnPoint){

        SpawnPoint.DirectionFacing directionFacing = roomSpawnPoint.GetRoomDirectionFacing();
        bool isOpen = roomSpawnPoint.GetIsOpen();

        foreach(GameObject room in rooms){
            List<SpawnPoint> roomSpawnPoints = new List<SpawnPoint>(room.GetComponent<RoomObject>().spawnPoints);

            int spawnPointIndex = 0;
            List<SpawnPoint> possibleSpawnPoints = new List<SpawnPoint>();
            List<int> possibleSpawnPointIndexes = new List<int>();

            foreach(SpawnPoint spawnPoint in roomSpawnPoints){

                if((spawnPoint.GetDirectionFacing() == directionFacing) && (spawnPoint.GetIsOpen() == isOpen)){
                    //Debug.Log("found the room pog "+ spawnPoint.GetDirectionFacing());
                    possibleSpawnPoints.Add(spawnPoint);
                    possibleSpawnPointIndexes.Add(spawnPointIndex);
                }
                spawnPointIndex++;
            }
            int randomRoom = Random.Range(0,possibleSpawnPoints.Count);
            spawnPointIndexReturn = possibleSpawnPointIndexes[randomRoom];
            return possibleSpawnPoints[randomRoom];

        }
        return rooms[0].GetComponent<RoomObject>().spawnPoints[0];
    }

    public GameObject RoomCreator(GameObject room, Vector3 pos, int deadSpawnPointIndex, bool doStuff){

        hardStop++;
        Debug.Log("total rooms " + hardStop);

        if(hardStop<hardStopMax){
            GameObject CreatedRoom = Instantiate(room, pos, Quaternion.identity);
            CreatedRoom.GetComponent<RoomObject>().Setup(doStuff,deadSpawnPointIndex);

            return CreatedRoom;
        }
        return null;

    }

    public int assignSpawnPointID(){
        currentSpawnPointID++;
        Debug.Log("assigned ID " + currentSpawnPointID);
        return currentSpawnPointID;
    }
}