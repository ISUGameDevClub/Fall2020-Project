using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryShop : MonoBehaviour
{
    public bool weapon;
    public int batteryCost;
    public BatteryInventory be;
    public AudioSource rejectSound;

    void Start()
    {
        be = FindObjectOfType<BatteryInventory>();
    }

    public bool CanPickup()
    {
        bool invFull = true;

        bool[] inv = FindObjectOfType<UI_Inventory>().isFull;
        foreach(bool slot in inv)
        {
            if (!slot)
                invFull = false;
        }

        if (BatteryInventory.batteries >= batteryCost && (!invFull || !weapon))
        {
            BatteryInventory.batteries -= batteryCost;
            return true;
        }
        AudioSource.PlayClipAtPoint(rejectSound.clip, transform.position);
        return false;
    }
}
