using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void healDamage(int d)
    {
        curHealth += d;

        if(curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
    }
    public void Takedamage(int d)
    {
        curHealth = curHealth - d;
        
        if(curHealth <= 0)
        {
            die();
        }
    }
    public void die()
    {
        Destroy(gameObject);
    }
}
