using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public AudioSource attackSound;
    public float attackLifespan;
    public float attackSpeed;
    public float spawnDistancefromPlayer;
    public int breakSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, attackLifespan);
    }
}
