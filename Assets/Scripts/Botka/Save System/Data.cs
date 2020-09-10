using UnityEngine;
using System.Collections;
using System.IO;
/**
 * @Author Jake Botka
 * 
 * A parent class to refrence all sub classes
 */
[System.Serializable]
public abstract class Data 
{
   
    public override string ToString()
    {
        return "Data : Not implemented to Strig in sub class";
    }

    public abstract int GetDataTypeCode();

    
}
