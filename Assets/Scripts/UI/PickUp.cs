using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private UI_Inventory inventory;
    public AudioSource pickupSound;
    public GameObject itemButton;
    public string weaponType;
    public WeaponInventory wi;
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<UI_Inventory>();
        wi = FindObjectOfType<WeaponInventory>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!GetComponent<BatteryShop>() || (GetComponent<BatteryShop>() && GetComponent<BatteryShop>().CanPickup()))
            {
                wi.AddWeapon(weaponType);
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    if (inventory.isFull[i] == false)
                    {
                        if (pickupSound != null)
                            AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
                        inventory.isFull[i] = true;
                        inventory.durs[i] = 100;
                        Instantiate(itemButton, inventory.slots[i].transform, false).GetComponent<SwitchWeapon>().myWeaponNumber = i;
                        Destroy(gameObject);
                        break;
                    }
                }
            }

        }
    }
}
