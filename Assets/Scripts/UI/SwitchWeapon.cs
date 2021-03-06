﻿using System.Collections;
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
    public string WeaponType;
    public int CurrentWeapon;
    private Text text;
    public int myWeaponNumber;
    public Sprite CharacterWithWeapon;
    public int myWeaponTypeNumber;
    public GameObject myDrop;
    
    public void Start()
    {
        wi = FindObjectOfType<WeaponInventory>();
        cw = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrentWeapon>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        CurrentWeapon = cw.SwitchWeapon;
        if(text != null && FindObjectOfType<UI_Inventory>() != null)
            text.text = FindObjectOfType<UI_Inventory>().durs[myWeaponNumber].ToString() +"%";
        AddWeapon(CurrentWeapon);
    }

    private void AddWeapon(int slot)
    {
        if(wi.weapons[slot] == WeaponType && pm != null)
        {
            pm.curAttack = WeaponBox;
            pm.GetComponent<SpriteRenderer>().sprite = CharacterWithWeapon;
        }  
    }
}
