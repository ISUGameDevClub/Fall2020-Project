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

    public enum RoomType
    {
        Normal, Special, Shop, Boss, NA
    }


    public const string _RoomShapeNullError = "Can not determine the room shape matrix.";
    public const string _RoomSettingsNullError = "Room settings script was left blank and could not be found in the heiarchy";
    public const float _PercentOffset = 0.14f;
    public const float _MinDistanceBetweenObjects = 0.50f;
    [Tooltip("This is the room shape matrix. Format as follows: # X #")]
    public int[] _RoomShape;
    public RoomType _RoomType;
    public Room_Settings _RoomSettings;
    public GameObject[,] _ObjectPlacementMatrix;
    public GameObject[] _Doors;
    public bool _ShowRoomCornersInScene;

    private GameObject[] _CornersInScene;

    [Header(" DO NOT SET")]
    public bool _Error;
    public Vector3[] _CornerPositions;
  

    /*
     * Called before first frame.
     * Same as initalization.
     */
     void Awake()
    {
        _Error = false;
        _ObjectPlacementMatrix = null;
        _CornerPositions = new Vector3[4];
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

    public bool IsBossRoom()
    {
        return _RoomType == RoomType.Boss;
    }


    public bool IsShopRoom()
    {
        return _RoomType == RoomType.Shop;
    }

    public bool IsSpecialRoom()
    {
        return _RoomType == RoomType.Special;
    }


    public bool IsNormalRoom()
    {
        return _RoomType == RoomType.Normal;
    }

}
