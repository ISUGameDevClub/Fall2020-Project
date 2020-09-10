using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
/**
 * @Athor Jake Botka - Programming Lead
 * Non monobehavior data container class that is utilized by the gamemanger monobehavior class
 */
public class CheckPointManager
{
   
    [SerializeField]private Text _CheckpointReachedText;
    private GameObject _Player;
    private int _CurrentIndex;
    private Checkpoint[] _LevelCheckPoints;

    public CheckPointData CurrentCheckpointData;

    public CheckPointManager(GameObject Player, Checkpoint[] checkpoints)
    {
        _Player = Player;
        _LevelCheckPoints = checkpoints;
        DifficultyScriptableObject difficulty;
        //difficulty = difficulty = GameManager.instance.GetGameDifficulty() != null ? difficulty : null; another way to assign two diffeerent values without calling twice
        //if ((difficulty = GameManager.instance.GetGameDifficulty()) != null) //assigns variable only if it does not equal null as well as checking if its null at the same time. If not done this way I would need to call getGameDifficulty twice. once for checking its null and once for assigning it to variable
       // {
         //   handleCheckpointDifficulty(difficulty);
      //  }

        
    }


    public void handleCheckpointDifficulty(DifficultyScriptableObject difficulty)
    {
        foreach (Checkpoint check in _LevelCheckPoints)
        {
            if (check.hasDifficulty(difficulty) == false)
            {
                check.gameObject.SetActive(false);
            }
        }
    }
  
    /**
     * 
     */
    public void SpawnPlayerAtCheckpoint()
    {
        if (_CurrentIndex != -1)
        {
            _Player.transform.position = _LevelCheckPoints[_CurrentIndex].gameObject.transform.position;
        }
    }

    /**
     * 
     */
    public void SpawnPlayerAtCheckpoint(int index)
    {
        if (_LevelCheckPoints != null)
        {
            if (_LevelCheckPoints.Length > 0)
            {
                _Player.transform.position = _LevelCheckPoints[index].gameObject.transform.position;
            }
        }

    }
  
    public void SpawnPlayerAtCheckpoint(Checkpoint checkpoint)
    {
        _Player.transform.position = checkpoint.gameObject.transform.position;
    }

    public Checkpoint GetCurrentChekcpint()
    {
        return _LevelCheckPoints[_CurrentIndex];
    }

    public void SetCheckpoint(Checkpoint value)
    {
        bool found = false;
        for (int i = 0; i < _LevelCheckPoints.Length; i++)
        {
            if (_LevelCheckPoints[i].GetInstanceID() == value.GetInstanceID() || _LevelCheckPoints[i].transform == value.transform)
            {
                found = true;
                _CurrentIndex = i;
            }
        }

        if (found == false)
        {
            Debug.LogWarning("Setting checkpoint not in checkpount array");
        }


        this.SetCheckpointData();
    }

    public void SetCheckpoint(CheckPointData value)
    {
        bool found = false;
        for (int i = 0; i < _LevelCheckPoints.Length; i++)
        {
            if (_LevelCheckPoints[i].gameObject.name == value.GetCheckpointName())
            {
                found = true;
                _CurrentIndex = i;
            }
        }

        if (found == false)
        {
            Debug.LogWarning("Setting checkpoint not in checkpount array");
        }

        this.SetCheckpointData();

    }
    public void SetCheckpoint(int index)
    {
        _CurrentIndex = index;
        this.SetCheckpoint(_LevelCheckPoints[_CurrentIndex]);
        
    }

    public void SetCheckpointData()
    {
        if (CurrentCheckpointData == null)
        {
           // CurrentCheckpointData = new CheckPointData(this.GetCurrentChekcpint(), GameManager.instance.Level.ToString());
        }
        CurrentCheckpointData.SetCheckpointName(this.GetCurrentChekcpint().gameObject.name);
       // GameManager.instance.GetFileManager().GetCurrentSaveFile().OverwriteData(CurrentCheckpointData, CheckPointData._DataTypeCode);

    }

    public void SetCheckpointData(CheckPointData data)
    {
        CurrentCheckpointData = data;
        this.SetCheckpoint(data);
        //GameManager.instance.GetFileManager().SaveFile((Data)CurrentCheckpointData);

    }


    public int GetCheckpointIndex()
    {
        return _CurrentIndex;
    }

}
