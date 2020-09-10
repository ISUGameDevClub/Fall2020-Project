using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Diffiluty_Settings", menuName ="Difficluty")]
public class DifficultyScriptableObject : ScriptableObject
{
   
    public enum Difficuty // change values accordingly
    {
        None = 0, // default value
        Easy,
        Medium,
        Hard,
        Expert
    }

    private Difficuty _SelectedDifficulty;

    public string GetName()
    {
        return _SelectedDifficulty.ToString();
    }
    public Difficuty GetDifficuty()
    {
        return _SelectedDifficulty;
    }

    public void setDifficulty(Difficuty value)
    {
        this._SelectedDifficulty = value;
    }

}
