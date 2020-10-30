using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float mainVolume;
    public float shopVolume;
    public float bossVolume;
    public float victoryVolume;
    public float musicFadeSpeed;
    public AudioSource mainTheme;
    public AudioSource shopTheme;
    public AudioSource bossTheme;
    public AudioSource victoryTheme;
    public string currentSong;
    private bool playingVictoryTheme;

    private bool bossThemeCanPlay;
    // Start is called before the first frame update
    void Start()
    {
        mainTheme.volume = 0;
        shopTheme.volume = 0;
        bossTheme.volume = 0;
        victoryTheme.volume = 0;
        playingVictoryTheme = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSong=="Main" && mainTheme.volume < mainVolume) 
        {
            mainTheme.volume += Time.deltaTime * musicFadeSpeed;
        }
        else if (currentSong=="Main")
        {
            mainTheme.volume = mainVolume; 
        }
        else if (mainTheme.volume > 0)
        {
            mainTheme.volume -= Time.deltaTime * musicFadeSpeed;
        }

        if (currentSong == "Shop" && shopTheme.volume < shopVolume)
        {
            shopTheme.volume += Time.deltaTime * musicFadeSpeed;
        }
        else if (currentSong == "Shop")
        {
            shopTheme.volume = shopVolume;
        }
        else if (shopTheme.volume > 0)
        {
            shopTheme.volume -= Time.deltaTime * musicFadeSpeed;

        }

        if (currentSong == "Boss" && bossTheme.volume < bossVolume)
        {
            if(!bossThemeCanPlay)
            {
                bossThemeCanPlay = true;
                bossTheme.Play();
            }
            bossTheme.volume += Time.deltaTime * musicFadeSpeed;
        }
        else if (currentSong == "Boss")
        {
            bossTheme.volume = bossVolume;
        }
        else if (bossTheme.volume > 0)
        {
            bossTheme.volume -= Time.deltaTime * musicFadeSpeed;
        }


        if (currentSong == "Victory")
        {
            victoryTheme.volume = victoryVolume;
            if (!playingVictoryTheme)
            {
                StartCoroutine(WaitForTheme());
            }
        }

    }

    IEnumerator WaitForTheme()
    {
        playingVictoryTheme = true;
        victoryTheme.Play();
        yield return new WaitForSecondsRealtime(4);
        foreach (PlayerInRoom piy in FindObjectsOfType<PlayerInRoom>())
        {
            if (piy.roomType == "Victory")
                piy.roomType = "Main";
        }
        currentSong = "Main";
        playingVictoryTheme = false;
    }
}
