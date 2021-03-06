﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private UI_Inventory inventory;
    public AudioSource pickupSound;
    public int itemButtonNumber;
    public string weaponType;
    public int dur = 100;
    public float justDropped;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<UI_Inventory>();
    }

    private void Update()
    {
        if(justDropped > 0)
        {
            justDropped -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (justDropped <= 0)
            {
                if (!GetComponent<BatteryShop>() || (GetComponent<BatteryShop>() && GetComponent<BatteryShop>().CanPickup()))
                {
                    if (inventory.GetWeapon(dur, weaponType, itemButtonNumber))
                    {
                        if (pickupSound != null)
                            AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);

                        if (!GetComponent<BatteryShop>() || !GetComponent<BatteryShop>().zMode)
                            Destroy(gameObject);
                        else
                            justDropped = 1;
                    }
                }
            }
        }
    }
}
