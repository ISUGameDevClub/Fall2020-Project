using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    private UI_Inventory inventory;
    public WeaponInventory wi;
    public int i;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<UI_Inventory>();
        wi = FindObjectOfType<WeaponInventory>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
            wi.DestroyWeapon(i);
        }
    }
}