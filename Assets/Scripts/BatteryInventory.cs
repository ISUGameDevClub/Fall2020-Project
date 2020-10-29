using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryInventory : MonoBehaviour
{

    public int batteryEnergy = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBattery(int batteryLife)
    {

        batteryEnergy += batteryLife;

    }
   
}
