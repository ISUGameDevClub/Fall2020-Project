using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberChip : MonoBehaviour
{
    private BatteryInventory bi;

    public bool speedBoost;
    public bool attackSpeedBoost;
    public bool batteryBoost;
    public bool healthBoost;
    public AudioSource pickupSound;

    private bool active;

    void Start()
    {
        bi = FindObjectOfType<BatteryInventory>();
        StartCoroutine(BecomeActive());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && active)
        {
            if(speedBoost)
                PlayerMovement.speedBoost = true;

            if (attackSpeedBoost)
                PlayerMovement.attackSpeedBoost = true;

            if(batteryBoost)
                bi.AddBattery(15);

            if (healthBoost)
            {
                PlayerMovement.maxHealth = 6;
                FindObjectOfType<PlayerMovement>().GetComponent<Health>().maxHealth = 6;
                FindObjectOfType<PlayerMovement>().GetComponent<Health>().HealDamage(100);
            }

            AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
            Destroy(gameObject);
        }
    }

    private IEnumerator BecomeActive()
    {
        yield return new WaitForSeconds(2);
        active = true;
    }
}
