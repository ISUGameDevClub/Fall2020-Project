using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<PickUp>() || collision.GetComponent<BatteryPickup>() || collision.GetComponent<HealthPack>())
        {
            collision.transform.position += transform.up * Time.deltaTime;
        }
    }
}
