using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Barrel" || collision.gameObject.tag == "Turret")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Shield")
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(collision.GetComponent<AudioSource>().clip, transform.position);
        }
    }
}
