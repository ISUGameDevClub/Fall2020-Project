using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryInventory : MonoBehaviour
{
    public Text batteryText;
    public static int batteries = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        batteryText.text = batteries.ToString();
    }

    public void AddBattery(int batteryLife)
    {
        batteries += batteryLife;
    }
   
}
