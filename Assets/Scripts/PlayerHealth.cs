using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    private Health h;
    public int numOfHearts;

    public Animator[] hearts;

    private void Start()
    {
        h = GetComponent<Health>();
    }

    private void Update()
    {
        health = h.curHealth;

        for (int i = 1; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].SetTrigger("Gain Heart");
            }
            else
            {
                hearts[i].SetTrigger("Lose Heart");
            }
        }
    }

    private void OnDestroy()
    {
        if (hearts[0] != null)
            hearts[0].SetTrigger("Lose Heart");
    }
}
