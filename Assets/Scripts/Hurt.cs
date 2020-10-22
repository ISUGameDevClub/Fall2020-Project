using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    public int damage;
    public bool PlayerATK;
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
        if(collision.gameObject.tag == "Enemy" && PlayerATK)
        {
            collision.gameObject.GetComponent<Health>().Takedamage(damage);
        }
        else if(collision.gameObject.tag == "Player" && !PlayerATK)
        {
            collision.gameObject.GetComponent<Health>().Takedamage(damage);
        }
    }
}
