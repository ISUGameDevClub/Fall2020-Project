using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public BatteryInventory bi;
    public int batteryLife = 1;
    void Start()
    {
      bi = FindObjectOfType<BatteryInventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
      {
          if(collision.gameObject.tag == "Player"){
            bi.AddBattery(batteryLife);
            Destroy(gameObject);
          }
      }
}
