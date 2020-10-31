using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuiting : MonoBehaviour
{
    public static bool gameEnding;
    private void OnApplicationQuit()
    {
        gameEnding = true;
    }
}
