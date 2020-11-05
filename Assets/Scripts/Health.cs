using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool isPlayer;
    public int maxHealth;
    public int curHealth;
    public float invincibilityTimer;
    private bool isInvincible;
    public AudioSource hurtSound;

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
        if (isPlayer)
        {
            GetComponent<Animator>().SetTrigger("Heal");
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            curHealth -= amount;
            StartCoroutine(PlayerHit());
            if (isPlayer)
            {
                GetComponent<Animator>().SetTrigger("Hurt");
            }
            if (hurtSound != null) { 
            AudioSource.PlayClipAtPoint(hurtSound.clip, transform.position);
            }
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
