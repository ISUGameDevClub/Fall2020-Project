using UnityEngine;
using System.Collections;
using System.IO;
using System;

[System.Serializable]
/**
    * @Author Jake Botka
    */
public abstract class SavableData
{
    protected string _Path;
    protected string _Ext;
    protected int _FileSize;
    [SerializeField] protected long _TimeStamp;
                     protected long _LastAccessed;
    [SerializeField]protected long _LastSaved;

    public SavableData()
    {
        _LastAccessed = (long)DateTime.Now.TimeOfDay.TotalMilliseconds;
    }




    public abstract Data[] CollectData(); //other data objects
    public abstract Data GetData(int DataCode);
    public abstract int GetDataTypeCode();

    public abstract void LoadDataFromFile();
    public abstract void SaveDataToFile(string file);
    public abstract void SaveDateToJsonFile();


    /**
     * 
     */
    public virtual  FileStream getDataStream()
    {
        return null;
    }
    /**
    * 
    */
    public virtual FileStream getFile()
    {
        return null;
    }
    /**
    * 
    */
    public virtual string GetFilePath()
    {
        return _Path;
    }

    public virtual string GetFileExt()
    {
        return _Ext;
    }

    

    public virtual FileDirectory GetDirectory()
    {
        return FileManager.GetDirectory();
    }

    public virtual int GetFileSize()
    {
        return _FileSize;
    }

    public override string ToString()
    {
        return base.ToString();
    }

   
}
