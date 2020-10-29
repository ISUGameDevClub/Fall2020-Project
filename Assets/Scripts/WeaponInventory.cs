using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public string[] weapons;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWeapon(string newWeapon)
    {
        for(int x = 0; x < weapons.Length; x++)
        {
            if (weapons[x] == "")
            {
                weapons[x] = newWeapon;
                x = weapons.Length;
            }
        }
    }
    public void DestroyWeapon(int CurrentWeapon)
    {
        weapons[CurrentWeapon] = "";
    }
}
