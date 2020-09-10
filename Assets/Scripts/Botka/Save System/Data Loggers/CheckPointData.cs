using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 
 * @Author Jake Botka
 */
 [System.Serializable]
public class CheckPointData : Data
{
   [SerializeField] private string _CheckpointName;
    [SerializeField] private string _SceneName;
    public const int _DataTypeCode = 241;
    
    public CheckPointData(Checkpoint checkpoint, string level)
    {
        this._CheckpointName = checkpoint.gameObject.name;
        this._SceneName = level;

    }


    public string GetCheckpointName()
    {
        return _CheckpointName;
    }

    public override int GetDataTypeCode()
    {
        return _DataTypeCode;
    }

    public void SetCheckpointName(string name)
    {
        this._CheckpointName = name;
    }


    public override string ToString()
    {
        return "\nLevel : " +  this._SceneName
            +"\n Checkpoint Name: " + this._CheckpointName
            + "\nPosition: " + null;
    }

}
