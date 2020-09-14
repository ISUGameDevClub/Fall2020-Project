using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * @Author JAke Botka
 */
public class Room_Settings : MonoBehaviour
{
    public int _TotalObjects;
    public int[] _RoomSizeMatrix;
    public GameObject[] _Prefabs;

    [Tooltip("The indexs of this list correspind to the indexes of the prefabs. " +
        "The number in each index is the amount of each prefab that you want to spawn")]
    public int[] _ObjectSpawnLimits;

    
    [Header("DEBUG _ DO NOT SET")]
    public bool _Error;
    public int _Test;


    void Awake()
    {
        _Error = false;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void FixedUpdate()
    {
        DataValidation();
    }

    public void DataValidation()
    {
        if (CheckAndNahdleNull(_RoomSizeMatrix))
        {
            Room room = GetComponentInParent<Room>();
            _RoomSizeMatrix = room != null ? room._RoomShape : null;
            CheckAndNahdleNull(_RoomSizeMatrix);
        }
        CheckAndNahdleNull(_Prefabs);
    }

    public bool CheckAndNahdleNull(object obj)
    {
        _Error = obj == null;
        return _Error;
    }

    

   
}
