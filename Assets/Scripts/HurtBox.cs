using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public int damage;
    public bool playerAttack;
    public bool constantDamage;

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
        }
    }

}
