using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public bool ResetGameWhenLoaded;

    private void Awake()
    {
        if(ResetGameWhenLoaded)
        {
            BatteryInventory.batteries = 0;
            UI_Inventory.lastDurs = new int[4];
            UI_Inventory.lastWeaponTypes = new string[4];
            UI_Inventory.lastItemButtonNums = new int[4];

            /*
            WeaponInventory.weapons = new string[4];
            for (int x = 0; x < WeaponInventory.weapons.Length; x++)
            {
                WeaponInventory.weapons[x] = "";
            }

            UI_Inventory.isFull = new bool[4];
            for (int x = 0; x < UI_Inventory.isFull.Length; x++)
            {
                UI_Inventory.isFull[x] = false;
            }

            UI_Inventory.durs = new int[4];
            for (int x = 0; x < UI_Inventory.durs.Length; x++)
            {
                UI_Inventory.durs[x] = 0;
            }
            */
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
