using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private UI_Inventory inventory;
    public AudioSource pickupSound;
    public int itemButtonNumber;
    public string weaponType;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<UI_Inventory>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!GetComponent<BatteryShop>() || (GetComponent<BatteryShop>() && GetComponent<BatteryShop>().CanPickup()))
            {
                if (inventory.GetWeapon(100, weaponType, itemButtonNumber))
                {
                    if (pickupSound != null)
                        AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
                    Destroy(gameObject);
                }
            }

        }
    }
}
