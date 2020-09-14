using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * @Author Jake Botka
 */
public class RoomSet : MonoBehaviour
{
    public const string _StartingRoomTag = "Room Start";
    public const string _RoomTag = "Room";
    public GameObject[] _RoomTypePrefabs;
    [Header("DO NOT SET")]
    public Room[] _Rooms;
    public Room _CurrentRoom;


    void Awake()
    {
        _Rooms = null;
        _CurrentRoom = null;
    }
    void Start()
    {
        _Rooms = gameObject.GetComponentsInChildren<Room>();
       Coroutine WaitCoroutine = StartCoroutine(WaitTillAllLoaded());
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator WaitTillAllLoaded()
    {
        yield return new WaitUntil(() => IsRoomsLoaded());
        Debug.Log("Rooms Loadded");
        if (_Rooms != null)
        {
            foreach (Room room in _Rooms)
            {
                if (room.gameObject.tag == _StartingRoomTag)
                {
                    Debug.Log(room.gameObject.tag);
                    ActivateRoom(room);
                    break;
                }
            }
        }
    }

    public bool IsRoomsLoaded()
    {
        foreach (Room room in _Rooms)
        {
            if (room.gameObject.tag == _StartingRoomTag)
            {
                if (room.enabled == false)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ActivateRoom(Room room)
    {
        room.enabled = true;
        DisableCurrentRoom();
        _CurrentRoom = room;

    }

    public void DisableCurrentRoom()
    {
        if (_CurrentRoom != null)
        {
            DisableRoom(_CurrentRoom);
        }
    }

    public void DisableRoom(Room room)
    {
        _CurrentRoom = null;
        room.gameObject.SetActive(false);
    }
}
