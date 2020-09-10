using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner
{
    public static GameObject SpawnGameObject(GameObject prefab, Vector3 position)
    {
        return SpawnGameObject(prefab, position, Quaternion.identity);
    }

    public static GameObject SpawnGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(prefab, position, rotation) as GameObject;
    }
}
