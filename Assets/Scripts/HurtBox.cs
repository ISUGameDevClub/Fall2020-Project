using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public int damage;
    public bool playerAttack;

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
        if(collision.gameObject.tag == "Enemy" && playerAttack)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        else if (collision.gameObject.tag == "Barrel" && playerAttack)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        else if(collision.gameObject.tag == "Player" && !playerAttack)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

}
