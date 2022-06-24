using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] string exitPos;
    [SerializeField] bool isOpen;
    [SerializeField] bool disable;

    [SerializeField] int spawnPointID;

    public enum DirectionFacing{up,down,left,right};
    [SerializeField] DirectionFacing directionFacing;
    DirectionFacing roomDirectionFacing;

    RoomLoader roomLoader;
    GameObject spawnedRoom;

    Vector3 roomSpawnPos;


    bool roomOverLap;
    SpawnPoint self;

    public void Start(){

        roomLoader = FindObjectOfType<RoomLoader>();
        
        spawnPointID = roomLoader.assignSpawnPointID();

        if(directionFacing == DirectionFacing.up){
            roomSpawnPos = new Vector3(transform.position.x,transform.position.y+5,transform.position.z);
            roomDirectionFacing = DirectionFacing.down;
        }
        else if(directionFacing == DirectionFacing.down){
            roomSpawnPos = new Vector3(transform.position.x,transform.position.y-5,transform.position.z);
            roomDirectionFacing = DirectionFacing.up;
        }
        else if(directionFacing == DirectionFacing.left){
            roomSpawnPos = new Vector3(transform.position.x-5,transform.position.y,transform.position.z);
            roomDirectionFacing = DirectionFacing.right;
        }
        else if(directionFacing == DirectionFacing.right){
            roomSpawnPos = new Vector3(transform.position.x+5,transform.position.y,transform.position.z);
            roomDirectionFacing = DirectionFacing.left;
        }
        else{
            roomSpawnPos = new Vector3(100,100,0);
        }
        // Debug.Log(roomOverLap);
        self = gameObject.GetComponent<SpawnPoint>();
    }

    public void ActivateSpawnPoint(){
        if(disable){
            return;
        }

        Debug.Log("spawnpoint active");

        SpawnPoint usedSpawnpoint = roomLoader.SpawnRoom(self);
        int usedSpawnpointIndex = roomLoader.spawnPointIndexReturn;
        
        GameObject spawedRoomPrefab = usedSpawnpoint.transform.parent.gameObject.transform.parent.gameObject;

        //Vector3 roomSpawnPos = new Vector3(transform.position.x*2,transform.position.y*2,transform.position.z);
        
        GameObject createdRoom = roomLoader.RoomCreator(spawedRoomPrefab, roomSpawnPos, usedSpawnpointIndex, true);
        //spawn actual room
        // Instantiate(spawnedRoom, transform.position, Quaternion.identity);

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Room"){
            roomOverLap = false;
            return;
        }

        roomOverLap = true;
    }

    public DirectionFacing GetDirectionFacing(){
        return directionFacing;
    }
    public DirectionFacing GetRoomDirectionFacing(){
        return roomDirectionFacing;
    }
    public bool GetIsOpen(){
        return isOpen;
    }
    public int ID(){
        return spawnPointID;
    }

    public void disableSpawnPoint(){disable = true;return;}
}
