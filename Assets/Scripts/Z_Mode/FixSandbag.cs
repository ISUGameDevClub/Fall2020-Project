using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSandbag : MonoBehaviour
{
    public Health he;
    public float timeBetweenFix;
    private float fixTimer;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            if (fixTimer <= 0)
            {
                fixTimer = timeBetweenFix;
                he.HealDamage(1);
            }
            else
                fixTimer -= Time.deltaTime;
        }
    }
}
