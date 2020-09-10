using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileDirectory
{
    private static FileDirectory _Instance = new FileDirectory();
    private const string _DirectoryForSavedData = "Assets/SavedData/";
    private const string _DirectoryForGameData = "Assets/SavedData/Game Data/";

    private FileDirectory()
    {

    }



    public static FileDirectory GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new FileDirectory();
        }
        return _Instance;
    }


    public string GetSaveDataDir()
    {
        return _DirectoryForSavedData;
    }

    public string GetGameDataDir()
    {
        return _DirectoryForGameData;
    }




    
}
