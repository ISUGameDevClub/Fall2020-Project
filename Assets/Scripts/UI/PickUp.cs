using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private UI_Inventory inventory;
<<<<<<< HEAD
    public AudioSource pickupSound;
    public int itemButtonNumber;
=======
    public GameObject itemButton;
>>>>>>> d8f934a9bedafd7e6df3a29a5952d9ad8ba196ed
    public string weaponType;

    private void Start()
    {
<<<<<<< HEAD
        if (GameObject.FindGameObjectWithTag("Player") != null)
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<UI_Inventory>();
=======
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<UI_Inventory>();
        wi = FindObjectOfType<WeaponInventory>();

>>>>>>> d8f934a9bedafd7e6df3a29a5952d9ad8ba196ed
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wi.AddWeapon(weaponType);
            for (int i = 0; i < inventory.slots.Length; i++)
            {
<<<<<<< HEAD
                if(inventory.GetWeapon(100, weaponType, itemButtonNumber))
                {
                    if (pickupSound != null)
                        AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
                    Destroy(gameObject);
=======
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);        
                    Destroy(gameObject);
                    break;
>>>>>>> d8f934a9bedafd7e6df3a29a5952d9ad8ba196ed
                }
            }

        }
    }
}
