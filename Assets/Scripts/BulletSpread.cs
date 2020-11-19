using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpread : MonoBehaviour
{
    public GameObject bullet;
    public float bulletsSpawned;
    public float maxSpread;

    void Start()
    {
        for(int x = 0; x <= bulletsSpawned; x++)
        {
            GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
            if(x % 2 == 0)
                bul.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + (Random.Range(0, maxSpread)));
            else
                bul.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - (Random.Range(0, maxSpread)));

        }

        Destroy(gameObject, .1f);
    }
}
