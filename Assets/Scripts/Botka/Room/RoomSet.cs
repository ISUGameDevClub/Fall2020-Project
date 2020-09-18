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

    public enum Direction
    {
        Null, Left,Right,Down,Up
    }


    public GameObject[] _RoomTypePrefabs;
    public int _NumberOfRooms;
    public Vector3 _Origin;
    [HideInInspector] public FloorMapper _FloorMapper;
    private Coroutine _WaitCoroutine;
    
    [Header("DO NOT SET")]
    public Room[] _Rooms;
    public List<Room> _GeneratedRoomsOrdered;
    public List<Room> _ActiveRooms;
    public List<Room> _ExploredRooms;
    public List<Room> _UnexploredRooms;
    public List<Room> _DestroyedRooms;
    public List<Direction> _DirectionPlacementHistroy;
    public Room _CurrentRoom;
    
    public bool[] _DirectionEnpointsBlocked;
    private Vector3 _LastDirectionPos;
    private Direction _LastDirection;



    void Awake()
    {
        _Rooms = null;
        _CurrentRoom = null;
        if (_NumberOfRooms < 0)
        {
            _NumberOfRooms = 0;
        }
        _LastDirectionPos = Vector3.zero;
        _GeneratedRoomsOrdered = new List<Room>();
        _ActiveRooms = new List<Room>();
        _ExploredRooms = new List<Room>();
        _UnexploredRooms = new List<Room>();
        _DestroyedRooms = new List<Room>();
        _DirectionPlacementHistroy = new List<Direction>();
        _LastDirection = Direction.Null;
    
        _DirectionEnpointsBlocked = new bool[4];
        _DirectionEnpointsBlocked[0] = false;
        _DirectionEnpointsBlocked[1] = false;
        _DirectionEnpointsBlocked[2] = false;
        _DirectionEnpointsBlocked[3] = false;




    }
    void Start()
    {
        _Rooms = new Room[_NumberOfRooms];
      _WaitCoroutine = StartCoroutine(WaitTillAllLoaded());
        _FloorMapper = GetComponentInChildren<FloorMapper>();
        GenerateMap();
       
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_WaitCoroutine == null)
        {

        }
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
        _WaitCoroutine = null;
        
    }

    public bool IsRoomsLoaded()
    {
        if (_Rooms != null)
        {
            foreach (Room room in _Rooms)
            {
                if (room != null)
                {
                    if (room.gameObject.tag == _StartingRoomTag)
                    {
                        if (room.enabled == false)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void GenerateMap()
    {
        if (_Rooms != null)
        {
            GameObject prefab = null;
            for (int i =0; i < _NumberOfRooms; i++)
            {
                if (IsAllEndpointsBlocked())
                {
                    return;
                }
                prefab = _RoomTypePrefabs[0]; // defaut
                //GET RANDOM ROOM
                if (i == 0)
                {
                    
                    _FloorMapper.Init(prefab);
                    AddRoom(i, prefab, GetNewPosition(null,prefab, _FloorMapper._BranchEndpoints[0]),prefab.GetComponentInChildren<Room>());
                   
                }
                else
                {
                    
                    GenerateRoom(i, prefab.GetComponentInChildren<Room>(), prefab, _GeneratedRoomsOrdered.ToArray()[_GeneratedRoomsOrdered.Count - 1].gameObject, Direction.Null);
                    
                }
                
            }
        }
    }

    public void GenerateRoom(int index, Room room, GameObject prefab, GameObject lastRoom, Direction direction)
    {
        Direction dir = direction;
        dir = dir != Direction.Null ? dir : ChooseSide();
        int i = -1;
        switch (dir) // switch direction
        {
            case Direction.Null:
                i = -1;
                break;
            case Direction.Left:
                i = 3;
                break;
            case Direction.Right:
                i = 1;
                break;
            case Direction.Up:
                i = 0;
                break;
            case Direction.Down:
                i = 2;
                break;
        }

        GameObject chosenPreviousRoom = null;
        if (!_DirectionEnpointsBlocked[i])
        {
            BranchEndPoint endPoint = _FloorMapper._BranchEndpoints[i];
            Debug.Log(endPoint);
            chosenPreviousRoom = _FloorMapper._BranchEndpoints[i]._EndPoint;
            Vector3 pos = GetNewPosition(chosenPreviousRoom, lastRoom,endPoint);
            if (pos != Vector3.zero)
            {
                lastRoom = endPoint._EndPoint;
                
                _FloorMapper.Add(endPoint, AddRoom(index, prefab, pos, room) ,dir);
            }
            else
            {
                _DirectionEnpointsBlocked[i] = true;
                GenerateRoom(index, room, prefab, lastRoom, ClockwiseChoice(dir));
            }
        }
        else
        {
            if (!IsAllEndpointsBlocked())
                GenerateRoom(index, room, prefab, lastRoom, ClockwiseChoice(dir));
            else
                Debug.Log("All endpoints are blocked");
        }
    }

    public bool IsBlocking(BranchEndPoint endpoint, Direction dir)
    {
        int[] x = _FloorMapper.GetIndecies(endpoint._EndPointIndeces, dir);
        return _FloorMapper.IsBLocking(x[0], x[1]);
        
    }

    public bool IsEndpointBlocking(GameObject endpoint)
    {
        int index = 0;
        foreach (BranchEndPoint endP in _FloorMapper._BranchEndpoints)
        {
            GameObject obj = endP._EndPoint;
            if (obj.name == endpoint.name)
            {
                return _DirectionEnpointsBlocked[index];
            }
            index++;
        }
        return false;
    }

    public bool IsAllEndpointsBlocked()
    {
        if (_DirectionEnpointsBlocked[0] == true && _DirectionEnpointsBlocked[1] == true && _DirectionEnpointsBlocked[2] == true && _DirectionEnpointsBlocked[3] == true  )
        {
            return true;
        }
        return false;
    }
    public GameObject AddRoom(int index, GameObject prefab, Vector3 pos,  Room room)
    {
        Debug.Log("Room Added");
        if (_Rooms != null && index >= 0)
        {
            _Rooms[index] = room;
            _GeneratedRoomsOrdered.Add(room);
            GameObject x = ObjectSpawner.SpawnGameObject(prefab, pos, Quaternion.identity);
                x.name += " - " + index;
            return x;
           
        }

        return null;
    }

    public void RemoveRoom(Room room)
    {
        
        Destroy(room.gameObject);
    }
    public Vector3 ProbeNewPosition(GameObject previousSpawn, Vector3 size2, Direction dir)
    {
        Vector3 size1 = previousSpawn.GetComponentInChildren<Renderer>().bounds.size;
        
        float unitsToMoveX = (size1.x / 2) + (size2.x / 2);
        float unitsToMoveY = (size1.y / 2) + (size2.y / 2);
        switch (dir)
        {
            case Direction.Null:
                break;
            case Direction.Left:
                return new Vector3(previousSpawn.transform.position.x - unitsToMoveX, previousSpawn.transform.position.y, previousSpawn.transform.position.z);
            case Direction.Right:
                return new Vector3(previousSpawn.transform.position.x + unitsToMoveX, previousSpawn.transform.position.y, previousSpawn.transform.position.z);
            case Direction.Up:
                return new Vector3(previousSpawn.transform.position.x, previousSpawn.transform.position.y + unitsToMoveY, previousSpawn.transform.position.z);
            case Direction.Down:
                return new Vector3(previousSpawn.transform.position.x, previousSpawn.transform.position.y - unitsToMoveY, previousSpawn.transform.position.z);
        }
        return Vector3.zero;
    }
    public Vector3 GetNewPosition(GameObject previousSpawn, GameObject needSpawn, BranchEndPoint endP)
    {
        if (previousSpawn == null)
            return _Origin;
        else
        {
            int count = 0;
            while (true)
            {
                Direction dir = Direction.Null;
                if (_LastDirection == Direction.Null)
                {
                    dir = Direction.Left;
                    _DirectionPlacementHistroy.Add(dir);
                    _LastDirection = dir;
                }
                else
                {
                   
                    dir = ClockwiseChoice(_LastDirection);
                    _LastDirection = dir;


                }

                int[] indeces = _FloorMapper.GetIndecies(endP._EndPointIndeces, dir);
                if (!_FloorMapper.IsBLocking(indeces[0], indeces[1]))
                {

                    Debug.Log( indeces[0] + "," + indeces[1]);
                    Vector3 pos = ProbeNewPosition(previousSpawn, needSpawn.GetComponentInChildren<Renderer>().bounds.size, dir);
                    return pos;
                }
                else if (count < 4)
                {
                    _LastDirection = ClockwiseChoice(_LastDirection);
                }
                else
                {
                    return Vector3.zero;
                }

                count++;
            }
        }
        //return Vector3.zero;
    }

    public Direction ChooseSide()
    {
        int num = Random.Range(0, 3);
        switch (num)
        {
            case 0:
                return Direction.Left;
            case 1:
                return Direction.Right;
            case 2:
                return Direction.Down;
            case 3:
                return Direction.Up;
            default:
                break;
        }
        
        return Direction.Null;
    }

    public Direction ClockwiseChoice(Direction prev)
    {
        Direction dir = prev;
        switch (dir) // switch direction
        {
            case Direction.Null:
                dir = Direction.Left;
                break;
            case Direction.Left:
                dir = Direction.Up;
                break;
            case Direction.Right:
                dir = Direction.Down;
                break;
            case Direction.Up:
                dir = Direction.Right;
                break;
            case Direction.Down:
                dir = Direction.Left;
                break;
        }
        return dir;
    }
    public void EnterRoom(Room oldRoom ,Room newRoom)
    {
        if (IsInList(_UnexploredRooms, newRoom))
        {
            _UnexploredRooms.Remove(newRoom);
            _ExploredRooms.Add(newRoom);
        }
        else
        {
            if (!IsInList(_ExploredRooms, newRoom))
            {
                _ExploredRooms.Add(newRoom);
            }
        }
    }

    public bool IsInList(List<Room> list, Room room)
    {
        foreach(Room r in list)
        {
            if (r.gameObject.name == room.gameObject.name)
            {
                return true;
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
