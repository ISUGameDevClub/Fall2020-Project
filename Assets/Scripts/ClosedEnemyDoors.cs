using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ClosedEnemyDoors : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] enemies;
    public bool roomInUse;
    public bool roomDone;

    public void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                return;
            }
        }
        roomDone = true;
        OpenDoors();
    }
    
    public void ActivateRoom(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Activate all enemies
            for (int i = 0; i < enemies.Length; i++)
            {
               // ChangeActivation(enemies[i], true);
            }
            CloseDoors();
        }
    }
    

    public void CloseDoors()
    {
        if (!roomDone)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
            }
            roomInUse = true;
        }
    }

    public void OpenDoors()
    {
        roomInUse = false;
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        int enemyCount = 0;
        foreach (Transform child in transform.parent)
        {
            if (child.CompareTag("Enemy"))
            {
                enemyCount++;
            }
        }
        enemies = new GameObject[enemyCount];
        enemyCount = 0;
        foreach (Transform child in transform.parent)
        {
            if (child.CompareTag("Enemy"))
            {
                enemies[enemyCount] = child.gameObject;
                enemyCount++;
            }
        }
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (roomInUse)
        {
            CheckEnemies();
        }
    }
}

