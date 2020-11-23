using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZCameraController : MonoBehaviour
{
    public float camSpeed;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        { 
            float dis = Vector2.Distance(player.transform.position, new Vector2(transform.position.x, transform.position.y));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), camSpeed * dis * Time.deltaTime);
        }
    }
}
