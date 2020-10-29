using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryShop : MonoBehaviour
{
    public BatteryInventory be;
    public int batteryTotal;
    void Start()
    {
        batteryTotal = be.batteryEnergy;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Remove(int batteryCost)
    {
        if (batteryTotal - batteryCost < 0)
        {
            batteryTotal = 0;
        }
        else
        {
            batteryTotal -= batteryCost;
        }
    }
}
