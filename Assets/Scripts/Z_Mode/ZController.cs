using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZController : MonoBehaviour
{
    public int currentWave;
    public float zombiesToSpawn;
    public float zombiesSpawned;
    public float zombiesKilled;
    public bool keepSpawning;
    public float spawnTimeReduction;
    public Text waveText;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (zombiesSpawned >= zombiesToSpawn)
            keepSpawning = false;

        if(!keepSpawning && zombiesKilled == zombiesSpawned)
        {
            NewWave();
        }
    }

    public void NewWave()
    {
        waveText.enabled = true;
        waveText.text = "Wave " + currentWave.ToString() + " Complete";
        StartCoroutine(EndText());
        currentWave++;
        if (spawnTimeReduction < 8)
            spawnTimeReduction = (currentWave - 1) * .5f;
        zombiesToSpawn += 4;
        keepSpawning = true;
        zombiesSpawned = 0;
        zombiesKilled = 0;

    }

    private IEnumerator EndText()
    {
        yield return new WaitForSeconds(3);
        waveText.enabled = false;
    }
}
