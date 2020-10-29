using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryShop : MonoBehaviour
{
    public int batteryCost;
    public BatteryInventory be;

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
        return false;
    }
}
