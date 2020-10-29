using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Globalization;

public class SwitchWeapon : MonoBehaviour
{
    private WeaponInventory wi;
    private CurrentWeapon cw;
    private PlayerMovement pm;
    public GameObject WeaponBox;
    public GameObject Attack;
    public string WeaponType;
    public int CurrentWeapon;
    public int Durability = 100;
    public int DurabilityReduce;
    public bool attack;
    public void Start()
    {
        wi = FindObjectOfType<WeaponInventory>();
        cw = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrentWeapon>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        CurrentWeapon = cw.SwitchWeapon;
        
        AddWeapon(CurrentWeapon);
        ReduceDurability(CurrentWeapon);
        attack = pm.attackReady;

    }
    private void AddWeapon(int slot)
    {
        if(wi.weapons[slot] == WeaponType)
        {
            pm.curAttack = WeaponBox;
        }
        
    }

    private void ReduceDurability(int slot)
    {
        if (wi.weapons[slot] == WeaponType && attack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Durability -= DurabilityReduce;
                if (Durability == 0)
                {
                    pm.curAttack = Attack;
                    Destroy(gameObject);
                }
            }
           
        }
    }

}
