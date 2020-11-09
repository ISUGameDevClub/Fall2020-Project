using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject deathParticle;
    public bool isPlayer;
    public int maxHealth;
    public int curHealth;
    public float invincibilityTimer;
    public bool isInvincible;
    public AudioSource hurtSound;
    public Slider healthBar;
    private float healthBarOffset;
    public bool showHealthToStart;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
            healthBarOffset = healthBar.transform.localPosition.y;
            healthBar.gameObject.transform.SetParent(null);
            if(!showHealthToStart)
                healthBar.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBar != null)
            healthBar.transform.position = transform.position + (new Vector3(0, healthBarOffset, 0) * transform.localScale.x);

        if (healthBar != null && curHealth != maxHealth)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.value = curHealth;
        }
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
        if (isPlayer)
        {
            FindObjectOfType<ScreenTransition>().FadeToDeath();
        }

        if (deathParticle != null)
            Instantiate(deathParticle, transform.position, new Quaternion(0,0,0,0));

        if (healthBar != null)
            Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }
}
