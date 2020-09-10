using UnityEngine;
using System.Collections;

public class ArrayUtil
{
    /**
     * Adds element at the end of the array by growing the array by one length
     */
    public static object[] AddElement(object[] Arr, object Obj)
    {
        int size = -1;
        object[] ArrTemp = null;
        if (Arr != null)
        {
            Arr = new object[0];
        }

        size = Arr.Length;
        ArrTemp = new object[size + 1]; // creates new array with one extra index at the end for the new element
        for (int i = 0; i < Arr.Length; i++) // copies current list
        {
            ArrTemp[i] = Arr[i];
        }
        ArrTemp[ArrTemp.Length - 1] = Obj; // adds the new elements to the end
        Arr = ArrTemp; // reassign back before returning

        ArrTemp = null; // enable for object destruction

        return Arr;
    }


}
