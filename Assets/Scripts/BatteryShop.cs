using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryShop : MonoBehaviour
{
    public int batteryCost;
    public BatteryInventory be;
    public AudioSource rejectSound;

    void Start()
    {
        be = FindObjectOfType<BatteryInventory>();
    }

    public bool CanPickup()
    {
        if (be.batteries >= batteryCost)
        {
            be.batteries -= batteryCost;
            return true;
        }
        AudioSource.PlayClipAtPoint(rejectSound.clip, transform.position);
        return false;
    }
}
