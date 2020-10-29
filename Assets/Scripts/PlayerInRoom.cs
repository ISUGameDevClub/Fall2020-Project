using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRoom : MonoBehaviour

{
    public string roomType;
    private GameObject player;
    private MusicManager mm; 

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        mm = FindObjectOfType<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && Mathf.Abs(player.transform.position.x-transform.position.x)< 9 && Mathf.Abs(player.transform.position.y - transform.position.y) < 5)
        {
            mm.currentSong = roomType;
        }
    }
}
