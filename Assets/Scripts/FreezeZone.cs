using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeZone : MonoBehaviour
{
    
    public float freezeTime;
<<<<<<< HEAD
    public GameObject freezeEffect;
    // Start is called before the first frame update
=======
>>>>>>> d8f934a9bedafd7e6df3a29a5952d9ad8ba196ed
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().stun = freezeTime;
            Instantiate(freezeEffect, collision.gameObject.transform.position, new Quaternion(0,0,0,0)).GetComponent<DestroyAfterTime>().lingerTime = freezeTime;
        }
        else if (collision.gameObject.tag == "Ememy")
        {
<<<<<<< HEAD
            if (collision.gameObject.GetComponent<MeleeAI>() != null)
            {
                collision.gameObject.GetComponent<MeleeAI>().stun = freezeTime;
                Instantiate(freezeEffect, collision.gameObject.transform.position, new Quaternion(0, 0, 0, 0)).GetComponent<DestroyAfterTime>().lingerTime = freezeTime;
            }
            else if (collision.gameObject.GetComponent<RangedAI>() != null)
            {
                collision.gameObject.GetComponent<RangedAI>().stun = freezeTime;
                Instantiate(freezeEffect, collision.gameObject.transform.position, new Quaternion(0, 0, 0, 0)).GetComponent<DestroyAfterTime>().lingerTime = freezeTime;
=======
            if (gameObject.GetComponent<MeleeAI>() != null)
            {
                collision.gameObject.GetComponent<MeleeAI>().stun = freezeTime;
            }
            if (gameObject.GetComponent<RangedAI>() != null)
            {
                collision.gameObject.GetComponent<RangedAI>().stun = freezeTime;
>>>>>>> d8f934a9bedafd7e6df3a29a5952d9ad8ba196ed
            }
        }
    }
}
