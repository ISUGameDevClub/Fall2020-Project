using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * @Author Jake Botka
 */
public class Room : MonoBehaviour
{
    private const string _RoomShapeNullError = "Can not determine the room shape matrix.";
    private const string _RoomSettingsNullError = "Room settings script was left blank and could not be found in the heiarchy";

    [Tooltip("This is the room shape matrix. Format as follows: # X #")]
    public int[] _RoomShape;
    public Room_Settings _RoomSettings;
    public GameObject[][] _ObjectPlacementMatrix;

    [Header("DEBUG _ DO NOT SET")]
    public bool _Error;

    /*
     * Called before first frame.
     * Same as initalization.
     */
     void Awake()
    {
        _Error = false;
    }

    /*
     * Called on first frame/
     */
    void Start()
    {
        //assigns room settings to itself if its not null itherwise it assign its the return value of getComponent
        _RoomSettings = _RoomSettings != null ? _RoomSettings : GetComponentInChildren<Room_Settings>();
        CheckAndHandleNull(_RoomSettings);

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

        CheckAndHandleNull(_RoomShape);
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
    [ContextMenu("Regenerate Roone")]
    public void GenerateRoon()
    {
        //TODO
        if (_RoomSettings != null)
        {
            int objectAmount = _RoomSettings._TotalObjects;
        }
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
}
