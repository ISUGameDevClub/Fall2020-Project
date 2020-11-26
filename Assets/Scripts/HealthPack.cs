using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public AudioSource healSound;

    public int healthHealed = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!GetComponent<BatteryShop>() || (GetComponent<BatteryShop>() && GetComponent<BatteryShop>().CanPickup()))
            {
                collision.gameObject.GetComponent<Health>().HealDamage(healthHealed);
                AudioSource.PlayClipAtPoint(healSound.clip, transform.position);
                if(!GetComponent<BatteryShop>() || (GetComponent<BatteryShop>() && !GetComponent<BatteryShop>().zMode))
                    Destroy(gameObject);
            }
        }
    }
}
