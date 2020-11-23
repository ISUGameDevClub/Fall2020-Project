using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool active;
    public GameObject[] spawns;
    private int curSpawn;
    public float timeBetweenSpawns;
    private float spawnTimer;
    public GameObject checkpoint;
    private ZController zc;

    private void Start()
    {
        zc = FindObjectOfType<ZController>();
        spawnTimer = spawnTimer = timeBetweenSpawns - zc.spawnTimeReduction + Random.Range(0, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (active && zc.keepSpawning)
        {
            if (spawnTimer > 0)
                spawnTimer -= Time.deltaTime;
            else
            {
                spawnTimer = timeBetweenSpawns - zc.spawnTimeReduction + Random.Range(0, 3f);
                zc.zombiesSpawned++;

                GameObject en = Instantiate(spawns[curSpawn], transform.position, new Quaternion(0, 0, 0, 0)).gameObject;
                if (en.GetComponent<MeleeAI>() != null)
                {
                    en.GetComponent<MeleeAI>().zMode = true;
                    en.GetComponent<MeleeAI>().checkpoint = checkpoint.transform.position;
                }

                if (en.GetComponent<RangedAI>() != null)
                {
                    en.GetComponent<RangedAI>().zMode = true;
                    en.GetComponent<RangedAI>().checkpoint = checkpoint.transform.position;
                }

                if (curSpawn < spawns.Length - 1)
                    curSpawn++;
                else
                    curSpawn = 0;
            }
        }
        else
        {
            spawnTimer = timeBetweenSpawns - zc.spawnTimeReduction + Random.Range(0, 3f);
            curSpawn = 0;
        }
    }
}
