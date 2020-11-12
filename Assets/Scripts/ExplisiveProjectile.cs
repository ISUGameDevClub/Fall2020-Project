using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplisiveProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float projectileSpeed = 8;
    public float projectileSpeedReduction = 1;
    public float explosionTimer = 1;
    public GameObject explosion;
    void Start()
    {
        Invoke("Explosive", explosionTimer);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
        projectileSpeed -= projectileSpeedReduction * Time.deltaTime;
    }

    private void Explosive()
    {
        Instantiate(explosion,transform.position,new Quaternion(0,0,0,0));
        Destroy(gameObject);
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Explosive();
        }

    }
   
}
