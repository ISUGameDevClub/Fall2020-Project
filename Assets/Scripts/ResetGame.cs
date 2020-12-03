using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public bool ResetGameWhenLoaded;
    public bool z;
    public static bool zMode;

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

            if (z)
                zMode = true;
            else
                zMode = false;
        }
    }

    public void SetZ()
    {
        zMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.I))
        {
            if (FindObjectOfType<BeatenGame>())
                FindObjectOfType<BeatenGame>().ResetGame();
        }
    }
}
