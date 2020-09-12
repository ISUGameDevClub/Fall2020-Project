using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
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
    public const float _MinDistanceBetweenObjects = 0.50f;
    [Tooltip("This is the room shape matrix. Format as follows: # X #")]
    public int[] _RoomShape;
    public Room_Settings _RoomSettings;
    public GameObject[,] _ObjectPlacementMatrix;
    public GameObject[] _Doors;
    public bool _ShowRoomCornersInScene;

    private GameObject[] _CornersInScene;

    [Header(" DO NOT SET")]
    public bool _Error;
    public Vector3[] _CornerPositions;
    public List<GameObject> _SpawnedObjects;
    public Vector3[] _SpawnedObjectPositions;
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
        _SpawnedObjects= new List<GameObject>();
        _SpawnedObjectPositions = null;
        _WallOffset = 4.0f; // amount of object lengths from walls
        _DoorOffset = 4.0f; // amount of object lengths from doors
        _MaxNumberOfBjects = FindMaxAMountOfObjectsThatCanFit(); //TODO
        _CornersInScene = null;

      
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
        this.enabled = false; //must be set active by RoomSet
    }

    /*
     * Called on every fixed frame.
     */
    void FixedUpdate()
    {
        DataValidation();
        if (_ShowRoomCornersInScene)
        {
            ShowRoomCornersInScene();
        }
        else
        {
            if (_CornersInScene != null)
            {
                foreach(GameObject obj in _CornersInScene)
                {
                    Destroy(obj);
                }
                _CornersInScene = null;
            }
        }
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
                        GameObject prefab = _RoomSettings._Prefabs[z];
                        Vector3 spawnPos = GenerateRandomPosition(prefab);
                        if (spawnPos != Vector3.zero)
                        {
                            //TODO: Validate position
                            int callstackRepeatCallCount = 0;
                            while (!IsGeneratePositionGood(prefab, spawnPos))
                            {
                                if (callstackRepeatCallCount >= 100)
                                {
                                    Debug.LogError("Exceeded calll stack for generating random positition. Object will not be spaned");
                                    spawnPos = Vector3.zero;
                                    break;
                                }
                                callstackRepeatCallCount++;
                                spawnPos = GenerateRandomPosition(prefab);
                            }
                            if (spawnPos != Vector3.zero) // same as null but Vector3 can not be null
                            {
                                SpawnObjectInScene(prefab, spawnPos, Quaternion.identity);
                                _SpawnedObjects.Add(prefab);
                                Debug.Log("Object Spawned");
                            }
                        }
                    }
                }

                _SpawnedObjectPositions = new Vector3[_SpawnedObjects.Count];
                int count = 0;
                foreach(GameObject obj in _SpawnedObjects)
                {
                    _SpawnedObjectPositions[count] = obj.transform.position;
                    count++;
                }
            }
        }
    }
    
    public bool IsGeneratePositionGood(GameObject obj, Vector3 position)
    {
        
        //DOuble checks that object is not spwning to close to walls
        float x = position.x + _WallOffset;
        float requiredDistanceX = 0.0f;
        float requiredDistanceY = 0.0f;
        foreach(GameObject gObject in _SpawnedObjects)
        {
            Vector2 prefabSize = obj.GetComponentInChildren<Renderer>().bounds.size;
            Vector2 gObjectSize = gObject.GetComponentInChildren<Renderer>().bounds.size;
            requiredDistanceX = (prefabSize.x / 2) + (gObjectSize.x / 2) + 0.2f;
            requiredDistanceY = (prefabSize.y / 2) + (gObjectSize.y / 2) + 0.2f;
            Debug.Log(gObject.name + ":" + requiredDistanceX + "," + requiredDistanceY);
            Vector3 pos = gObject.transform.position;
            if (Math.Abs(position.x - gObject.transform.position.x) <= requiredDistanceX
                && Math.Abs(position.x - gObject.transform.position.x) <= requiredDistanceY)
            {
                return false;
            }
           
        }
        return true;
        
    }
    public Vector3 GenerateRandomPosition(GameObject obj)
    {
        float objectWallOffsetX = (obj.GetComponent<Renderer>().bounds.size.x / 2) * _WallOffset;
        float objectWallOffsetY = (obj.GetComponent<Renderer>().bounds.size.y / 2) * _WallOffset;
        Debug.Log("Generated random pos");
        if (_CornerPositions != null)
        {
            
            Vector3 pos = new Vector3(Random.Range(GetRoomCornerPosition(Corners.TopLeft).x + objectWallOffsetX, GetRoomCornerPosition(Corners.TopRight).x -objectWallOffsetX),
                                        Random.Range(GetRoomCornerPosition(Corners.TopLeft).y - objectWallOffsetY, GetRoomCornerPosition(Corners.BottomLeft).y + objectWallOffsetY), 
                                        -1);
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
        GameObject obj = ObjectSpawner.SpawnGameObject(prefab, position, rotation);
        obj.transform.parent = gameObject.transform;
        return obj;
    }
    public void CheckAndHandleNull(object obj)
    {
        _Error = obj == null;
    }

    public void ShowRoomCornersInScene()
    {
        if (_ShowRoomCornersInScene && _CornersInScene == null)
        {
            _CornersInScene = new GameObject[4];
            _CornersInScene[0] = SpawnObjectInScene(_RoomSettings._Prefabs[0], _CornerPositions[0], Quaternion.identity);
            _CornersInScene[0].name = "Corner Piece";
            _CornersInScene[1] = SpawnObjectInScene(_RoomSettings._Prefabs[0], _CornerPositions[1], Quaternion.identity);
            _CornersInScene[1].name = "Corner Piece";
            _CornersInScene[2] = SpawnObjectInScene(_RoomSettings._Prefabs[0], _CornerPositions[2], Quaternion.identity);
            _CornersInScene[2].name = "Corner Piece";
            _CornersInScene[3] = SpawnObjectInScene(_RoomSettings._Prefabs[0], _CornerPositions[3], Quaternion.identity);
            _CornersInScene[3].name = "Corner Piece";
        }
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
