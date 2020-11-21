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
            PlayerMovement.speedBoost = false;
            PlayerMovement.attackSpeedBoost = false;
            PlayerMovement.maxHealth = 5;
            Timer.seconds = 0;
            Timer.minutes = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
