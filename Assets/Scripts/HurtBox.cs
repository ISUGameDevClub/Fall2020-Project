using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public bool meleeAttack;
    public int damage;
    public bool playerAttack;
    public bool constantDamage;
    public bool passOverDestructible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!constantDamage)
        {
            if (collision.gameObject.tag == "Enemy" && playerAttack)
            {
                if((meleeAttack && CheckLineOfSight(collision)) || !meleeAttack)
                    collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            else if (collision.gameObject.tag == "Barrel" && playerAttack)
            {
                if ((meleeAttack && CheckLineOfSight(collision)) || !meleeAttack)
                    collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            else if (collision.gameObject.tag == "Player" && !playerAttack)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            else if (collision.gameObject.tag == "Destructible" && !passOverDestructible)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (constantDamage)
        {
            if (collision.gameObject.tag == "Enemy" && playerAttack)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            else if (collision.gameObject.tag == "Barrel" && playerAttack)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            else if (collision.gameObject.tag == "Player" && !playerAttack)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            else if (collision.gameObject.tag == "Destructible" && !passOverDestructible)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private bool CheckLineOfSight(Collider2D collision)
    {
        Vector3 origin = transform.position - transform.right;
        Vector3 target = collision.gameObject.transform.position;

        int layer_mask = LayerMask.GetMask("Default");
        Vector3 direction = (origin - target);
        RaycastHit2D hit = Physics2D.Raycast(target, direction, 100, layer_mask);
        if (hit.collider.gameObject.tag == "Player")
            return true;
        else
            return false;
    }

}
