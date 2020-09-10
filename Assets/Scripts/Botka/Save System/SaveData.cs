using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System;
/**
* @Author Jake Botka
* 
* Generic type that can represent any serilized date to any single file
*/
[System.Serializable]
public class SaveData : SavableData
{
    public const string _JsonRedableFile = "Readble";
   
    [SerializeField] private string _JsonReadablePath;
    private byte[] mSaveDataPaylod;
    [SerializeField]private List<Data> _AllDataInSlot;
    public Data[] _DataArray;
    private string _JsonText;
   
 
    //private string mFilePath = Application.persistentDataPath + "/gamesave.save";

    public SaveData(string PathFile)
    {
        this._Path = PathFile;

        if (this._AllDataInSlot == null) // if not serilized exmpt
        {
            this._AllDataInSlot = new List<Data>(0);
        }

        _DataArray = null;

        int index = _Path.LastIndexOf(".");
       _JsonReadablePath = _Path.Insert(index, _JsonRedableFile);
        this._JsonText = "";

     
       

    }

    public SaveData(bool x)
    {
        _DataArray = null;
        if (this._AllDataInSlot == null) // if not serilized exmpt
        {
            this._AllDataInSlot = new List<Data>(0);
        }
        _Path = GetDirectory() + StringUtils.GenerateID(9) + ".json";
        Debug.LogWarning(_Path);
       

    }

    public SaveData(string Path, Data[] data)
    {
        _DataArray = null;
        _Path = Path;
        if (this._AllDataInSlot == null) // if not serilized exmpt
        {
            this._AllDataInSlot = new List<Data>(0);
        }
        foreach (Data d in data)
        {
            _AllDataInSlot.Add(d);
        }

       


    }

    public void AddData(Data data)
    {
        if (_AllDataInSlot == null)
        {
            _AllDataInSlot = new List<Data>(0);
        }
            
            _AllDataInSlot.Add(data);
        
        // to do dicern data f between stuff like checkpoint
        _DataArray = new Data[1];
        _DataArray[0] = data;
      //  _DataArray = new Data[_AllDataInSlot.Count];

       // _DataArray = _AllDataInSlot.ToArray();



        this.SaveDateToJsonFile();
    }

    public void OverwriteData(Data data, int dataTypeCode)
    {
       // _AllDataInSlot.Clear();
        //this.SaveDateToJsonFile();
        Data[] arr = _AllDataInSlot.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            if (data.GetDataTypeCode() == arr[i].GetDataTypeCode())
            {
                Debug.Log("Found data at : " + i);
                _AllDataInSlot.RemoveAt(i);
                break;
            }
        }

        this.AddData(data);

    }

    /**
     * 
     */
    public override Data GetData(int DataCode)
    {
        foreach(Data data in _AllDataInSlot)
        {
            if (data.GetDataTypeCode() == DataCode)
            {
                return data;
            }
        }

        return null; //no data with datacode
    }

    /**
     * 
     */
    public override Data[] CollectData()
    {
        return _AllDataInSlot.ToArray();
    }

    /**
     * 
     */
    public override void LoadDataFromFile()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream _DataStream = File.Open(_Path, FileMode.OpenOrCreate);
        bf.Deserialize(_DataStream);
        _DataStream.Flush();
        _DataStream.Close();
        
    }

  
    /**
     * 
     */
    public override void SaveDataToFile(string file)
    {
        
        _Ext = ".json";
        throw new System.NotImplementedException();
    }

    /**
     * 
     */
    public void SaveJsonStringToJsonFile()
    {
        FileStream _DataStream = null;
        if (File.Exists(_JsonReadablePath))
        {
            _DataStream = File.Open(_JsonReadablePath, FileMode.OpenOrCreate);
        }
        else
        {
            Debug.Log("File does not exists , creating new file. Path : " + _JsonReadablePath);
            _DataStream = File.Create(_JsonReadablePath);
        }
    
        // System.IO.File.WriteAllText(_JsonReadablePath, _JsonText);

        // wrtie json text to file
        if (_DataStream != null)
        {
            _DataStream.Flush();
            _DataStream.Close();
            Debug.Log("Data sucessfuly saved");
        }

        base._LastSaved = (long)DateTime.Now.TimeOfDay.TotalMilliseconds;

    }

    /**
     * 
     */
    public override void SaveDateToJsonFile()
    {

        FileStream _DataStream = null;
        if (File.Exists(_Path))
        {
            _DataStream = File.Open(_Path, FileMode.OpenOrCreate);
        }
        else
        {
            Debug.Log("File does not exists , creating new file. Path : " + _Path);
            _DataStream = File.Create(_Path);
        }

        
        


        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(_DataStream, this);
        

        Debug.Log("Data saved to : " + _DataStream.Name);
        _DataStream.Flush();
        _DataStream.Close();

        this.SaveJsonStringToJsonFile();

        _Ext = ".json";
        
    }

    public Data[] GetAllData()
    {
        return _AllDataInSlot.ToArray();
    }


    public override string ToString()
    {
        string str = "Data Info: ";

        
        if (_AllDataInSlot != null)
        {
            foreach (Data data in _AllDataInSlot) // calls overriden to string method
            {
                str += "\nData Array: " + data.ToString();  // calls overriden to string method ov Data subclass. For example although refrenced as Data will call CheckPointData toString method if it is the sub of this data instance object
            }

            
            return str;
        }

        return "No Data Store in save data slot";
    }

    public override int GetDataTypeCode()
    {
        return -1;
    }

 
}
