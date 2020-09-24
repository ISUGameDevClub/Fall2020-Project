using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string weaponType;
    public WeaponInventory wi;

    void Start()
    {
        wi = FindObjectOfType<WeaponInventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wi.AddWeapon(weaponType);

            Destroy(gameObject);
        }
    }
}
