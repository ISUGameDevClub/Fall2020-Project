using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Last Saved Data Container")]
public class LastSaveFileScriptableObject: ScriptableObject
{
    [SerializeField] private string _FilePath;
    public LastSaveFileScriptableObject(string FilePath)
    {
        this.SetPath(FilePath);
    }


    public void SetPath(string path)
    {
        _FilePath = path;
    }

    public string GetPath()
    {
        return _FilePath;
    }


    
}
