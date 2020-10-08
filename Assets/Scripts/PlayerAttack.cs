using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackLifespan;
    public float attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, attackLifespan);
    }
}
