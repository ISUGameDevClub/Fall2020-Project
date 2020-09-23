using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * @Author Jake Botka
 */
public class Door : MonoBehaviour
{
    public BoxCollider2D _TriggerCollider;
    [Header("DEBUG_ DO NOT SET")]
    public RoomSet _RoomManager;
    public Room[] _BindingRooms;
    public bool _Locked;
    public bool _AcivitationDoorTrigger;
    void Awake()
    {
        _BindingRooms = new Room[2];
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_TriggerCollider == null)
        {
            _TriggerCollider = GetComponentInChildren<BoxCollider2D>();
            _TriggerCollider.isTrigger = true;
        }

        if (_RoomManager == null)
        {
            _RoomManager = GetComponentInParent<RoomSet>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterThroughDoor(Room origin)
    {
        Room room = null;
        foreach (Room r in _BindingRooms)
        {
            if (r != null)
            {
                if (origin.name != r.name)
                {
                    room = r;
                }
            }
        }

        if (_RoomManager != null)
        {
            _RoomManager.EnterRoom(origin, room);
        }
    }

    public void SetRooms(Room room1, Room room2)
    {
        _BindingRooms[0] = room1;
        _BindingRooms[1] = room2;
    }
    public bool IsDoorLocked()
    {
        return _Locked;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
