using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public bool ResetGameWhenLoaded;

    void Start()
    {
        if(ResetGameWhenLoaded)
        {
            BatteryInventory.batteries = 0;
            //WeaponInventory.weapons = new string[4];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
