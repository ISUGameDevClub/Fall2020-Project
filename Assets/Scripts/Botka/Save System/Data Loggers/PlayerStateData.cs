using UnityEngine;
using System.Collections;
[System.Serializable]
public class PlayerStateData : Data
{
    [SerializeField] private float _CurrentHealth;
    public PlayerStateData(float currentHealth)
    {
        _CurrentHealth = currentHealth;
    }
    public override int GetDataTypeCode()
    {
        return -1;
    }

  
}
