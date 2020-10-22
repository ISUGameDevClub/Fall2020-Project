using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  
    public float attackSpeed;
    public float speed;
    public float attackLife;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, attackLife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
