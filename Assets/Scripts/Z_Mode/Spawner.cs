using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool active;
    public GameObject[] spawns;
    public GameObject[] spawns2;
    public GameObject[] spawns3;
    private int curSpawn;
    public float timeBetweenSpawns;
    private float spawnTimer;
    public GameObject checkpoint;
    private ZController zc;
    public int waveLevel;

    private void Start()
    {
        zc = FindObjectOfType<ZController>();
        spawnTimer = spawnTimer = timeBetweenSpawns - zc.spawnTimeReduction + Random.Range(0, 3f);
        waveLevel = 1;
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
                GameObject en = null;
                if (waveLevel == 1)
                    en = Instantiate(spawns[curSpawn], transform.position, new Quaternion(0, 0, 0, 0)).gameObject;
                else if (waveLevel == 2)
                    en = Instantiate(spawns2[curSpawn], transform.position, new Quaternion(0, 0, 0, 0)).gameObject;
                else if (waveLevel == 3)
                    en = Instantiate(spawns3[curSpawn], transform.position, new Quaternion(0, 0, 0, 0)).gameObject;

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

                if (waveLevel == 1)
                { 
                    if (curSpawn < spawns.Length - 1)
                        curSpawn++;
                    else
                        curSpawn = 0;
                }
                else if (waveLevel == 2)
                {
                    if (curSpawn < spawns2.Length - 1)
                        curSpawn++;
                    else
                        curSpawn = 0;
                }
                else if (waveLevel == 3)
                {
                    if (curSpawn < spawns3.Length - 1)
                        curSpawn++;
                    else
                        curSpawn = 0;
                }
            }
        }
        else
        {
            spawnTimer = timeBetweenSpawns - zc.spawnTimeReduction + Random.Range(0, 3f);
            curSpawn = 0;
        }
    }
}
