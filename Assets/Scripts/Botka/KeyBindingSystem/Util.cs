using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * @Author JAke Botka
 */
public class Util
{
   public static Vector2 VectorConvertTo(Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }

    public static Vector3 VectorConvertTo(Vector2 vec, float z)
    {
        return new Vector3(vec.x, vec.y,z);
    }
}
