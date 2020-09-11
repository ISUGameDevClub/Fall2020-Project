using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * @Author Jake Botka
 */
public class Room : MonoBehaviour
{
    public enum Corners
    {
        TopLeft, TopRight, BottomLeft, BottomRight
    }

    public const string _RoomShapeNullError = "Can not determine the room shape matrix.";
    public const string _RoomSettingsNullError = "Room settings script was left blank and could not be found in the heiarchy";
    public const float _PercentOffset = 0.14f;
    public const float _MinDistanceBetweenObjects = 2.0f;
    [Tooltip("This is the room shape matrix. Format as follows: # X #")]
    public int[] _RoomShape;
    public Room_Settings _RoomSettings;
    public GameObject[,] _ObjectPlacementMatrix;
    public GameObject[] _Doors;

    [Header(" DO NOT SET")]
    public bool _Error;
    public Vector3[] _CornerPositions;
    public List<Vector3> _SpawnedObjectPositions;
    public float _WallOffset;
    public float _DoorOffset;
    public int _MaxNumberOfBjects;

    /*
     * Called before first frame.
     * Same as initalization.
     */
     void Awake()
    {
        _Error = false;
        _ObjectPlacementMatrix = null;
        _CornerPositions = new Vector3[4];
        _SpawnedObjectPositions = new List<Vector3>();
        _WallOffset = 0.0f;
        _DoorOffset = 0.0f;
        _MaxNumberOfBjects = FindMaxAMountOfObjectsThatCanFit(); //TODO

      
    }

    /*
     * Called on first frame/
     */
    void Start()
    {
        //assigns room settings to itself if its not null itherwise it assign its the return value of getComponent
        _RoomSettings = _RoomSettings != null ? _RoomSettings : GetComponentInChildren<Room_Settings>();
        CheckAndHandleNull(_RoomSettings);
        Vector3 size = GetComponent<Renderer>().bounds.size;
        _CornerPositions[0] = new Vector3(transform.position.x - (size.x / 2),
            transform.position.y - (size.y / 2 ));
        _CornerPositions[1] = new Vector3(transform.position.x + (size.x / 2),
            transform.position.y - (size.y / 2));
        _CornerPositions[2] = new Vector3(transform.position.x - (size.x / 2),
            transform.position.y + (size.y / 2));
        _CornerPositions[3] = new Vector3(transform.position.x + (size.x / 2),
            transform.position.y + (size.y / 2));
        _WallOffset = size.x * _PercentOffset;
        _DoorOffset = size.x * _PercentOffset;
        SpawnObjectInScene(_RoomSettings._Prefabs[0], _CornerPositions[0], Quaternion.identity).name = "Corner Piece";
        SpawnObjectInScene(_RoomSettings._Prefabs[0], _CornerPositions[1], Quaternion.identity).name = "Corner Piece";
        SpawnObjectInScene(_RoomSettings._Prefabs[0], _CornerPositions[2], Quaternion.identity).name = "Corner Piece";
        SpawnObjectInScene(_RoomSettings._Prefabs[0], _CornerPositions[3], Quaternion.identity).name = "Corner Piece";

        if (_RoomShape == null)
        {
            if (_RoomSettings != null)
            {
                _RoomShape = _RoomSettings._RoomSizeMatrix;
            }

            if (_RoomShape == null) // if still equals null
            {
                //TODO
            }
        }
        else
        {
            _RoomShape = _RoomSettings._RoomSizeMatrix;
        }

        CheckAndHandleNull(_RoomShape);
        _ObjectPlacementMatrix = new GameObject[_RoomShape[0], _RoomShape[1]];
        GenerateRoom();
    }

    /*
     * Called on every fixed frame.
     */
    void FixedUpdate()
    {
        DataValidation();
    }

    /*
     * Validates data.
     * Checks if any erorrs. 
     * Checks nulls
     */
    public void DataValidation()
    {
        CheckAndHandleNull(_RoomShape);
        CheckAndHandleNull(_RoomSettings);
    }
    
    /*
     * Generates Room.
     */
    [ContextMenu("Regenerate Room")]
    public void GenerateRoom()
    {
        //TODO
        if (_RoomSettings != null)
        {
            int objectAmount = _RoomSettings._TotalObjects;
            int[] objectMatrix = _RoomSettings._ObjectSpawnLimits;
            Debug.Log(objectMatrix[1]);
            if (objectMatrix != null)
            {
                for (int i = 0; i < objectMatrix.Length; i++)
                {
                    for (int z = 0; z < objectMatrix[i]; z++)
                    {
                        Vector3 spawnPos = GenerateRandomPosition();
                        if (spawnPos != Vector3.zero)
                        {
                            //TODO: Validate position
                            int callstackRepeatCallCount = 0;
                            while (!IsGeneratePositionGood(spawnPos))
                            {
                                if (callstackRepeatCallCount >= 100)
                                {
                                    Debug.LogError("Exceeded calll stack for generating random positition. Object will not be spaned");
                                    spawnPos = Vector3.zero;
                                    break;
                                }
                                callstackRepeatCallCount++;
                                spawnPos = GenerateRandomPosition();
                            }
                            if (spawnPos != Vector3.zero) // same as null but Vector3 can not be null
                            {
                                SpawnObjectInScene(_RoomSettings._Prefabs[z], spawnPos, Quaternion.identity);
                                _SpawnedObjectPositions.Add(spawnPos);
                                Debug.Log("Object Spawned");
                            }
                        }
                    }
                }
            }
        }
    }
    
    public bool IsGeneratePositionGood(Vector3 position)
    {
        //DOuble checks that object is not spwning to close to walls
        float x = position.x + _WallOffset;
        if (position.x >= GetRoomCornerPosition(Corners.TopLeft).x + _WallOffset
            && position.x <= GetRoomCornerPosition(Corners.TopRight).x - _WallOffset
            && position.y >= GetRoomCornerPosition(Corners.BottomLeft).y + _WallOffset
            && position.y <= GetRoomCornerPosition(Corners.TopLeft).y - _WallOffset)
        {
            foreach(Vector3 pos in _SpawnedObjectPositions)
            {
                
                if (Vector2.Distance(Util.VectorConvertTo(pos), Util.VectorConvertTo(position)) < _MinDistanceBetweenObjects)
                {
                    return false;
                }
                Debug.Log(Vector3.Distance(pos, position));
            }
            return true;
        }
        return false;
    }
    public Vector3 GenerateRandomPosition()
    {
        Debug.Log("Generated random pos");
        if (_CornerPositions != null)
        {
            
            Vector3 pos = new Vector3(Random.Range(GetRoomCornerPosition(Corners.TopLeft).x + _WallOffset, GetRoomCornerPosition(Corners.TopRight).x - _WallOffset),
                                        Random.Range(GetRoomCornerPosition(Corners.TopLeft).y - _WallOffset, GetRoomCornerPosition(Corners.BottomLeft).y + _WallOffset), 
                                        transform.position.z);
            Debug.Log("Random position in room constraints: " + pos);
            return pos;
        }
        return Vector3.zero;
        
    }

    public Vector3 GetRoomCornerPosition(Corners corner)
    {
        if (_CornerPositions != null)
        {
            switch (corner)
            {
                case Corners.TopLeft:
                    return _CornerPositions[2];
                case Corners.TopRight:
                    return _CornerPositions[3];
                case Corners.BottomLeft:
                    return _CornerPositions[0];
                case Corners.BottomRight:
                    return _CornerPositions[1];

            }
        }
        return Vector3.zero;
    }
    public GameObject SpawnObjectInScene(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return ObjectSpawner.SpawnGameObject(prefab, position, rotation);
    }
    public void CheckAndHandleNull(object obj)
    {
        _Error = obj == null;
    }
    [ContextMenu("Print error message to console")]
    public void PrinttErrorMessage()
    {
        if (_Error)
        {
            //TODO
            if (_RoomSettings == null)
                Debug.LogError(_RoomSettingsNullError);
            if (_RoomShape == null)
                Debug.LogError(_RoomShapeNullError);
        }
        else
            Debug.Log("No Error is present");
    }

    public int FindMaxAMountOfObjectsThatCanFit()
    {
        //TODO
        return -1;
    }
}
