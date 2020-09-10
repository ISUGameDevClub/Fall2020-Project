using UnityEngine;
using System.Collections;
[System.Serializable]
public class PlayerInfoData : Data
{
    public const int _PlayerInfoDataCode = 24;

    [SerializeField] private PlayerStatsData _PlayerStatsData;
    [SerializeField] private PlayerStateData _PlayerStateInfo;

    public PlayerInfoData(PlayerStatsData PlayerStatsData, PlayerStateData PlayerStateData)
    {
        _PlayerStatsData = PlayerStatsData;
    }
    public override int GetDataTypeCode()
    {
        return _PlayerInfoDataCode;
    }
}

    

