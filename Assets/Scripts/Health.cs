using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth, curHealth;
    public bool isPlayer;
    public float invincibilityTimer;
    private bool isInvincible;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealDamage(int amount)
    {
        curHealth += amount;

        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
    }
    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            curHealth -= amount;
            StartCoroutine(PlayerHit());
        }
        

        if(curHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator PlayerHit()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTimer);
        isInvincible = false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
