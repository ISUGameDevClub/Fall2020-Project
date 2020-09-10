using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

/**
 * @Author Jake Botka
 */
public class FileManager
{
    private LastSaveFileSeiptableObject _LastSaveFileObject;
    private SaveData[] _SaveDatas;
    private SavableData[] _SavableResourceFiles;
    private SaveData _CurrentSaveFile;
    private string[] _SaveFilePaths; // this must be seperate even though inside of mSaveData because mSavadata itself is loaded with file
    // Use this for initialization
    /**
     * 
     */
    public FileManager()
    {
        if (_LastSaveFileObject == null)
        {
            // handle
        }

       
    
       _SaveFilePaths = LoadFileStreams(); //loads file paths fro directry
        _SavableResourceFiles = LoadSavedData(_SaveFilePaths); //loads SavableData objects from files in direcotries
        _SaveDatas = this.ExtractSaveDatas(_SavableResourceFiles); // Extracts SaveData objects from SavableData Objects
        if (_SaveDatas != null)
        {
            if (_SaveDatas.Length > 0)
            {
                _CurrentSaveFile = _SaveDatas[0];
            }
            else
            {
                LoadLastSavedData();
                _SaveDatas = (SaveData[])ArrayUtil.AddElement(_SaveDatas, _CurrentSaveFile);
            }
        }
        else
        {
            LoadLastSavedData();
            _SaveDatas = new SaveData[1];
            _SaveDatas = (SaveData[])ArrayUtil.AddElement(_SaveDatas, _CurrentSaveFile);
        }

        
    }


    public void LoadLastSavedData()
    {
        if (_LastSaveFileObject != null)
        {
            _CurrentSaveFile = new SaveData(_LastSaveFileObject.GetPath());
        }
        else
        {
            this._CurrentSaveFile = new SaveData(FileManager.GetDirectory().GetSaveDataDir() + "test.json");
            // handle ignore above line.
        }
    }

    

  /**
   * Retrieves all files and its file URI to late be handled
   */
    public string[] LoadFileStreams()
    {
        DirectoryInfo dir = new DirectoryInfo(FileManager.GetDirectory().GetSaveDataDir()); // get directory information
        FileInfo[] info = dir.GetFiles("*.json*"); // get files
        List<string> list = new List<string>(0);
        foreach (FileInfo f in info)
        {
            
            if (f.Extension != ".meta")
            {
                list.Add(f.FullName);
                Debug.Log(f.FullName);
            }

            
            
        }
        return list.ToArray();

    }

    /**
     * Loads savable data from file file directory represneted in individual file paths
     */
    public SavableData[] LoadSavedData(string[] paths)
    {

        SavableData[] data = null;
        int index = 0;
        if (paths != null)
        {
            BinaryFormatter bf = new BinaryFormatter();
            data = new SavableData[paths.Length];
            foreach (string path in paths)
            {
                FileStream stream = File.Open(path, FileMode.OpenOrCreate,FileAccess.Read ,FileShare.Read);
                if (stream.Length > 0)
                {
                    object obj = bf.Deserialize(stream);
                   
                     
                        if (obj is SavableData)
                        {
                            Debug.Log(path);
                            data[index] = (SavableData)obj;
                            
                            Debug.Log("Loaded data:" + data[index].ToString()); // calls overidden tostring method in SavabaleData that is further overidden by SaveData which is further overidden to its data containers implementation. This is benifit from polymorphisms
                         
                            index++; //incremenet
                        }
                        else if (1 == 2) // other inheriting members of data if applicable, Always fails conditional here move to else
                        {
                            // other inheriting object casted and handled here
                        }
                        else
                        {
                            Debug.LogWarning("Initialized data with no inheriting subclass, Path to file: " + path);
                            //just a data object no inheriting class, through away

                        }
                    
                    stream.Flush();
                }
                stream.Close();
            }
           
        }

        return data;
    }
    /**
     * Extrast save data from savabke resource data files
     */
    public SaveData[] ExtractSaveDatas(SavableData[] data)
    {
        List<SaveData> list = new List<SaveData>(0);
        foreach(SavableData sData in data)
        {
            if (sData is SaveData)
            {
                list.Add((SaveData)sData);
            }
            
        }
        if (list.Count > 0)
        {
            return list.ToArray();
        }

        return null;

    }

    /**
     * Tests to see if data type is an instance of SavabaleData
     * If it is then calls the SaveFile method with the casted object to its sub form.
     */
    public void SaveFile(Data data)
    {
       

    }

    /**
    * Calls overidden method to Serialize data to file
    */
    public void SaveFile(SavableData data)
    {
        
            data.SaveDateToJsonFile();
     
    }

    /**
     * 
     */
    public void saveFile(FileStream _DataStream)
    {
        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(_DataStream, this);
        Debug.Log(_DataStream.Length);
        _DataStream.Flush();
        _DataStream.Close();
       

    }
    /**
     * 
     */
    public static FileDirectory GetDirectory()
    {
        return FileDirectory.GetInstance(); // my singleton
    }


    public SaveData GetCurrentSaveFile()
    {
        return this._CurrentSaveFile;
    }
        

}
