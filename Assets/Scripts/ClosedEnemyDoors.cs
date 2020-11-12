using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedEnemyDoors : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] closedDoors;
    public GameObject[] enemies;
    public GameObject[] turrets;
    public bool roomInUse;
    public bool roomDone;

    public float enemySleepTime;

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

        int turretCount = 0;
        foreach (Transform child in transform.parent)
        {
            if (child.CompareTag("Turret"))
            {
                turretCount++;
            }
        }
        turrets = new GameObject[turretCount];
        turretCount = 0;
        foreach (Transform child in transform.parent)
        {
            if (child.CompareTag("Turret"))
            {
                turrets[turretCount] = child.gameObject;
                turretCount++;
            }
        }
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        }

        for (int i = 0; i < closedDoors.Length; i++)
        {
            closedDoors[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        for (int x = 0; x < enemies.Length; x++)
        {
            if (enemies[x].GetComponent<RangedAI>() != null)
                enemies[x].GetComponent<RangedAI>().enabled = false;
            if (enemies[x].GetComponent<MeleeAI>() != null)
                enemies[x].GetComponent<MeleeAI>().enabled = false;
            if (enemies[x].GetComponent<RiotBossAI>() != null)
                enemies[x].GetComponent<RiotBossAI>().enabled = false;
            if (enemies[x].GetComponent<ExplosiveBossAI>() != null)
                enemies[x].GetComponent<ExplosiveBossAI>().enabled = false;
        }
        for (int x = 0; x < turrets.Length; x++)
        {
            if (turrets[x].GetComponent<RangedAI>() != null)
                turrets[x].GetComponent<RangedAI>().enabled = false;
        }

        StartCoroutine(CheckForRooms());
    }

    // Update is called once per frame
    void Update()
    {
        if (roomInUse)
        {
            CheckEnemies();
        }
    }

    private IEnumerator CheckForRooms()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < closedDoors.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(closedDoors[i].transform.position + (closedDoors[i].transform.right * 2), closedDoors[i].transform.up, 1);
            //Debug.DrawRay(closedDoors[i].transform.position + (closedDoors[i].transform.right * 2), closedDoors[i].transform.up, Color.green, 1111, false);
            if (hit.collider != null)
            {
                closedDoors[i].GetComponent<SpriteRenderer>().enabled = false;
                doors[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                doors[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
                closedDoors[i].GetComponent<SpriteRenderer>().enabled = true;
            }

        }
    }

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

            for (int i = 0; i < closedDoors.Length; i++)
            {
                closedDoors[i].GetComponent<SpriteRenderer>().enabled = true;
            }

            StartCoroutine(EnableEnemiesAfterTime());
            roomInUse = true;
        }
    }

    private IEnumerator EnableEnemiesAfterTime()
    {
        yield return new WaitForSeconds(enemySleepTime);
        for (int x = 0; x < enemies.Length; x++)
        {
            if(enemies[x] != null && enemies[x].GetComponent<RangedAI>() != null)
                enemies[x].GetComponent<RangedAI>().enabled = true;
            if (enemies[x] != null && enemies[x].GetComponent<MeleeAI>() != null)
                enemies[x].GetComponent<MeleeAI>().enabled = true;
            if (enemies[x] != null && enemies[x].GetComponent<RiotBossAI>() != null)
                enemies[x].GetComponent<RiotBossAI>().enabled = true;
            if (enemies[x] != null && enemies[x].GetComponent<ExplosiveBossAI>() != null)
                enemies[x].GetComponent<ExplosiveBossAI>().enabled = true;
        }
        for (int x = 0; x < turrets.Length; x++)
        {
            if (turrets[x].GetComponent<RangedAI>() != null)
                turrets[x].GetComponent<RangedAI>().enabled = true;
            yield return new WaitForSeconds(2);
        }
    }

    public void OpenDoors()
    {
        roomInUse = false;
        /*
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        }
        */

        for (int i = 0; i < closedDoors.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(closedDoors[i].transform.position + (closedDoors[i].transform.right * 2), closedDoors[i].transform.up, 1);
            //Debug.DrawRay(closedDoors[i].transform.position + (closedDoors[i].transform.right * 2), closedDoors[i].transform.up, Color.green, 1111, false);
            if (hit.collider != null)
            {
                closedDoors[i].GetComponent<SpriteRenderer>().enabled = false;
                doors[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
            }
        }
        for (int x = 0; x < turrets.Length; x++)
        {
            if (turrets[x].GetComponent<RangedAI>() != null)
                turrets[x].GetComponent<RangedAI>().enabled = false;
        }
    }
}

